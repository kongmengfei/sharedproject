
import os
from config import config
from shareplum import Site
from shareplum import Office365
from shareplum.site import Version

# get data from configuration
username = config['sp_user']
password = config['sp_password']

authcookie = Office365('https://xxx.sharepoint.com', username=username, password=password).GetCookies()

site = Site('https://xxx.sharepoint.com/sites/testprivate',version=Version.v365, authcookie=authcookie)
spfolder = site.Folder('Shared Documents/allf')

for root, dirs, files in os.walk(r"D:\d365\SDK\SampleCode\JS\SOAPForJScript\SOAPForJScript"): 
    for file in files:
        filepath = os.path.join(root, file)
        print(filepath)

        # perform the actual upload
        with open(filepath, 'rb+') as file_input:
            try: 
                spfolder.upload_file(file_input, file)
            except Exception as err: 
                print("Some error occurred: " + str(err))
