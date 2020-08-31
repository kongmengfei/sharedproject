
from shareplum import Site
from shareplum import Office365
from shareplum.site import Version
from config import config

# get data from configuration
username = config['sp_user']
password = config['sp_password']

authcookie = Office365('https://xxx-my.sharepoint.com', username=username, password=password).GetCookies()
site = Site('https://xxx-my.sharepoint.com/personal/abc_xxx_onmicrosoft_com',version=Version.v365, authcookie=authcookie)
sp_list = site.List('Documents')
data = sp_list.GetListItems('All',row_limit=10)

print(data)
