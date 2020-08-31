
from config import config
from shareplum import Site
from shareplum import Office365
from shareplum.site import Version

# get data from configuration
username = config['sp_user']
password = config['sp_password']

authcookie = Office365('https://xxxxxx.sharepoint.com', username=username, password=password).GetCookies()

site = Site('https://xxxxxx.sharepoint.com/sites/sbdev/subtest01',version=Version.v365, authcookie=authcookie)

newlist=site.AddList('My New List', description='Great List!', template_id='Document Library')

print(newlist)
