
import json

from office365.runtime.auth.authentication_context import AuthenticationContext
from office365.runtime.client_request import ClientRequest
from office365.runtime.utilities.request_options import RequestOptions

tenant_url= "https://{tenant}.sharepoint.com"
ctx_auth = AuthenticationContext(tenant_url)

site_url="https://{tenant}.sharepoint.com/sites/{yoursite}"

if ctx_auth.acquire_token_for_user("username","password"):
  request = ClientRequest(ctx_auth)
  options = RequestOptions("{0}/_api/web/".format(site_url))
  options.set_header('Accept', 'application/json')
  options.set_header('Content-Type', 'application/json')
  data = request.execute_request_direct(options)
  s = json.loads(data.content)
  web_title = s['Title']
  print("Web title: " + web_title)
else:
  print(ctx_auth.get_last_error())
