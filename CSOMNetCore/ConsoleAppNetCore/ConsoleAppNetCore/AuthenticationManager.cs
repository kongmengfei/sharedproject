using Microsoft.SharePoint.Client;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleAppNetCore
{
    public class AuthenticationManager : IDisposable
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const string tokenEndpoint = "https://login.microsoftonline.com/common/oauth2/token";

        private const string defaultAADAppId = "ac3f2c12-c87d-42d1-84f7-1cbbba0c33c2x"; //986002f6-c3f6-43ab-913e-78cca185c392

        // Token cache handling
        private static readonly SemaphoreSlim semaphoreSlimTokens = new SemaphoreSlim(1);
        private AutoResetEvent tokenResetEvent = null;
        private readonly ConcurrentDictionary<string, string> tokenCache = new ConcurrentDictionary<string, string>();
        private bool disposedValue;

        internal class TokenWaitInfo
        {
            public RegisteredWaitHandle Handle = null;
        }

        public ClientContext GetContext(Uri web, string userPrincipalName, SecureString userPassword)
        {
            var context = new ClientContext(web);

            context.ExecutingWebRequest += (sender, e) =>
            {
                string accessToken = EnsureAccessTokenAsync(new Uri($"{web.Scheme}://{web.DnsSafeHost}"), userPrincipalName, new System.Net.NetworkCredential(string.Empty, userPassword).Password).GetAwaiter().GetResult();
                e.WebRequestExecutor.RequestHeaders["Authorization"] = "Bearer " + accessToken;
            };

            return context;
        }


        public async Task<string> EnsureAccessTokenAsync(Uri resourceUri, string userPrincipalName, string userPassword)
        {
            string accessTokenFromCache = TokenFromCache(resourceUri, tokenCache);
            if (accessTokenFromCache == null)
            {
                await semaphoreSlimTokens.WaitAsync().ConfigureAwait(false);
                try
                {
                    // No async methods are allowed in a lock section
                    string accessToken = await AcquireTokenAsync(resourceUri, userPrincipalName, userPassword).ConfigureAwait(false);
                    Console.WriteLine($"Successfully requested new access token resource {resourceUri.DnsSafeHost} for user {userPrincipalName}");
                    AddTokenToCache(resourceUri, tokenCache, accessToken);

                    // Register a thread to invalidate the access token once's it's expired
                    tokenResetEvent = new AutoResetEvent(false);
                    TokenWaitInfo wi = new TokenWaitInfo();
                    wi.Handle = ThreadPool.RegisterWaitForSingleObject(
                        tokenResetEvent,
                        async (state, timedOut) =>
                        {
                            if (!timedOut)
                            {
                                TokenWaitInfo wi = (TokenWaitInfo)state;
                                if (wi.Handle != null)
                                {
                                    wi.Handle.Unregister(null);
                                }
                            }
                            else
                            {
                                try
                                {
                                    // Take a lock to ensure no other threads are updating the SharePoint Access token at this time
                                    await semaphoreSlimTokens.WaitAsync().ConfigureAwait(false);
                                    RemoveTokenFromCache(resourceUri, tokenCache);
                                    Console.WriteLine($"Cached token for resource {resourceUri.DnsSafeHost} and user {userPrincipalName} expired");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Something went wrong during cache token invalidation: {ex.Message}");
                                    RemoveTokenFromCache(resourceUri, tokenCache);
                                }
                                finally
                                {
                                    semaphoreSlimTokens.Release();
                                }
                            }
                        },
                        wi,
                        (uint)CalculateThreadSleep(accessToken).TotalMilliseconds,
                        true
                    );

                    return accessToken;

                }
                finally
                {
                    semaphoreSlimTokens.Release();
                }
            }
            else
            {
                Console.WriteLine($"Returning token from cache for resource {resourceUri.DnsSafeHost} and user {userPrincipalName}");
                return accessTokenFromCache;
            }
        }

        private async Task<string> AcquireTokenAsync(Uri resourceUri, string username, string password)
        {
            string resource = $"{resourceUri.Scheme}://{resourceUri.DnsSafeHost}";

            var clientId = defaultAADAppId;
            var body = $"resource={resource}&client_id={clientId}&grant_type=password&username={HttpUtility.UrlEncode(username)}&password={HttpUtility.UrlEncode(password)}";
            using (var stringContent = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded"))
            {

                var result = await httpClient.PostAsync(tokenEndpoint, stringContent).ContinueWith((response) =>
                {
                    return response.Result.Content.ReadAsStringAsync().Result;
                }).ConfigureAwait(false);

                var tokenResult = JsonSerializer.Deserialize<JsonElement>(result);
                var token = tokenResult.GetProperty("access_token").GetString();
                return token;
            }
        }

        private static string TokenFromCache(Uri web, ConcurrentDictionary<string, string> tokenCache)
        {
            if (tokenCache.TryGetValue(web.DnsSafeHost, out string accessToken))
            {
                return accessToken;
            }

            return null;
        }

        private static void AddTokenToCache(Uri web, ConcurrentDictionary<string, string> tokenCache, string newAccessToken)
        {
            if (tokenCache.TryGetValue(web.DnsSafeHost, out string currentAccessToken))
            {
                tokenCache.TryUpdate(web.DnsSafeHost, newAccessToken, currentAccessToken);
            }
            else
            {
                tokenCache.TryAdd(web.DnsSafeHost, newAccessToken);
            }
        }

        private static void RemoveTokenFromCache(Uri web, ConcurrentDictionary<string, string> tokenCache)
        {
            tokenCache.TryRemove(web.DnsSafeHost, out string currentAccessToken);
        }

        private static TimeSpan CalculateThreadSleep(string accessToken)
        {
            System.IdentityModel.Tokens.Jwt.JwtSecurityToken token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(accessToken);
            var lease = GetAccessTokenLease(token.ValidTo);
            lease = TimeSpan.FromSeconds(lease.TotalSeconds - TimeSpan.FromMinutes(5).TotalSeconds > 0 ? lease.TotalSeconds - TimeSpan.FromMinutes(5).TotalSeconds : lease.TotalSeconds);
            return lease;
        }

        private static TimeSpan GetAccessTokenLease(DateTime expiresOn)
        {
            DateTime now = DateTime.UtcNow;
            DateTime expires = expiresOn.Kind == DateTimeKind.Utc ? expiresOn : TimeZoneInfo.ConvertTimeToUtc(expiresOn);
            TimeSpan lease = expires - now;
            return lease;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (tokenResetEvent != null)
                    {
                        tokenResetEvent.Set();
                        tokenResetEvent.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}