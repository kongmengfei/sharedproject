from office365.runtime.auth.authentication_context import AuthenticationContext
from office365.sharepoint.client_context import ClientContext
from office365.sharepoint.file_creation_information import FileCreationInformation
import os

tenant_url= "https://xxxxx.sharepoint.com"
site_url="https://xxxx.sharepoint.com/sites/testprivate"

ctx_auth = AuthenticationContext(tenant_url)
ctx_auth.acquire_token_for_user("xxx@xxx.onmicrosoft.com","xxxx") 


ctx = ClientContext(site_url, ctx_auth)

path = r"D:\dest.txt"

with open(path, 'rb') as content_file:
    file_content = content_file.read()

list_title = "Documents"
target_folder = ctx.web.lists.get_by_title(list_title).rootFolder

info = FileCreationInformation()
info.content = file_content
info.url = os.path.basename(path)
info.overwrite = True

target_file = target_folder.files.add(info)
ctx.execute_query()

print(target_file)
   
