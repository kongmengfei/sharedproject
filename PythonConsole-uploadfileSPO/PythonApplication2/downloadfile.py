
from shareplum import Site
from shareplum import Office365
from shareplum.site import Version
from config import config

# get data from configuration
username = config['sp_user']
password = config['sp_password']
site_name = config['sp_site_name']
base_path = config['sp_base_path']
doc_library = config['sp_doc_library']

authcookie = Office365(base_path, username=username, password=password).GetCookies()
site = Site('https://my.sharepoint.com/sites/sbdev',version=Version.v365, authcookie=authcookie)

folder = site.Folder('Shared Documents/This Folder')
folder.download_file('source.txt', 'destination.txt')
