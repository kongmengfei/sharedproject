using System;
using System.Net;

namespace Wictor.Office365
{
    public class ClaimsWebClient : WebClient {
        private readonly MsOnlineClaimsHelper claimsHelper;

        public ClaimsWebClient(Uri host, string username, string password) {
            claimsHelper = new MsOnlineClaimsHelper(host, username, password);
        }
        protected override WebRequest GetWebRequest(Uri address) {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest) {
                (request as HttpWebRequest).CookieContainer = claimsHelper.CookieContainer;
            }
            return request;
        }
    }
}
