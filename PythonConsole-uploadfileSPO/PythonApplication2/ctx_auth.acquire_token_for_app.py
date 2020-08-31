from office365.runtime.auth.authentication_context import AuthenticationContext
from office365.sharepoint.client_context import ClientContext
from office365.sharepoint.file import File

app_settings = {
     'url': 'https://xxxx.sharepoint.com/sites/testprivate',
     'client_id': '3601b9xxxxf8b67e3a',
     'client_secret': '4qr3urbxxxxEUESzc13Ys=',
}

ctx_auth = AuthenticationContext(url=app_settings['url'])
ctx_auth.acquire_token_for_app(client_id=app_settings['client_id'], client_secret=app_settings['client_secret'])

ctx = ClientContext(app_settings['url'], ctx_auth)

path = "D:\dest.txt"
response = File.open_binary(ctx, "/sites/testprivate/Shared%20Documents/source.txt")
response.raise_for_status()
with open(path, "wb") as local_file:
    local_file.write(response.content)

print('f')
