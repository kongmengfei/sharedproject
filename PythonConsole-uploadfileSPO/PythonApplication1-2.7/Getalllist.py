from office365.runtime.auth.authentication_context import AuthenticationContext
from office365.sharepoint.client_context import ClientContext

tenant_url= "https://company.sharepoint.com"
site_url="https://company.sharepoint.com/sites/sbdev"


ctx_auth = AuthenticationContext(tenant_url)


if ctx_auth.acquire_token_for_user("abc@company.onmicrosoft.com","mypassword"):
   ctx = ClientContext(site_url, ctx_auth)
   lists = ctx.web.lists
   ctx.load(lists)
   ctx.execute_query()

   for l in lists:
       print(l.properties["Title"])

else:
   print(ctx_auth.get_last_error())

