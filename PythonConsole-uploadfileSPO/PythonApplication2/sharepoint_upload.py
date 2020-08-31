
import requests
from shareplum import Office365
from config import config

# get data from configuration
username = config['sp_user']
password = config['sp_password']
site_name = config['sp_site_name']
base_path = config['sp_base_path']
doc_library = config['sp_doc_library']

file_name = "test.html"

# Obtain auth cookie
authcookie = Office365(base_path, username=username, password=password).GetCookies()
session = requests.Session()
session.cookies = authcookie
session.headers.update({'user-agent': 'python_bite/v1'})
session.headers.update({'accept': 'application/json;odata=verbose'})

# dirty workaround.... I'm getting the X-RequestDigest from the first failed call
session.headers.update({'X-RequestDigest': 'FormDigestValue'})
response = session.post( url=base_path + "/sites/" + site_name + "/_api/web/GetFolderByServerRelativeUrl('" + doc_library + "')/Files/add(url='a.txt',overwrite=true)",
                         data="")
session.headers.update({'X-RequestDigest': response.headers['X-RequestDigest']})

# perform the actual upload
with open( r'C:\Users\mengfeik\Documents\test.html', 'rb+') as file_input:
    try: 
        response = session.post( 
            url=base_path + "/sites/" + site_name + "/_api/web/GetFolderByServerRelativeUrl('" + doc_library + "')/Files/add(url='" 
            + file_name + "',overwrite=true)",
            data=file_input)
    except Exception as err: 
        print("Some error occurred: " + str(err))

print('end...')