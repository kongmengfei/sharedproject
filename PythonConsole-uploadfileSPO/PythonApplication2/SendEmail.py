
import json

from office365.runtime.auth.authentication_context import AuthenticationContext
from office365.runtime.client_request import ClientRequest
from office365.runtime.http.request_options import RequestOptions
from office365.runtime.http.http_method import HttpMethod
from office365.sharepoint.client_context import ClientContext

tenant_url= "https://abc.sharepoint.com"
site_url="https://abc.sharepoint.com/sites/s01"


ctx_auth = AuthenticationContext(tenant_url)

if ctx_auth.acquire_token_for_user("admin@abc.onmicrosoft.com","xxxxx"):
  request = ClientRequest(ctx_auth)

  # get digest
  optiondigest = RequestOptions("{0}/_api/contextinfo".format(site_url))
  optiondigest.method= HttpMethod.Post
  optiondigest.set_header('Accept', 'application/json')
  optiondigest.set_header('Content-Type', 'application/json')

  datadigest = request.execute_request_direct(optiondigest)
  s = json.loads(datadigest.content)
  FormDigestValue = s['FormDigestValue']
  print("FormDigestValue"+ FormDigestValue)

  # Send email
  options = RequestOptions("{0}/_api/SP.Utilities.Utility.SendEmail".format(site_url))
  options.set_header('Accept', 'application/json')
  options.set_header('Content-Type', 'application/json;odata=verbose')
  options.set_header('X-RequestDigest', FormDigestValue)

  options.method= HttpMethod.Post
  options.data= {
   "properties":{
      "__metadata":{
         "type":"SP.Utilities.EmailProperties"
      },
      "To":{
         "results":[
            "user01@abc.onmicrosoft.com"
         ]
      },
      "Body":"Body01",
      "Subject":"SubjectOfEmail01"
   }
}

  data = request.execute_request_direct(options)
  se = json.loads(data.content)
  print(se)
else:
  print(ctx_auth.get_last_error())
