Add-Type -Path 'C:\Program Files\Common Files\microsoft shared\Web Server Extensions\16\ISAPI\Microsoft.SharePoint.Client.dll'

#Mysite URL
$site = 'https://abc.sharepoint.com/sites/sbdev'

#Admin User Principal Name
$admin = 'admin@abc.onmicrosoft.com'

#Get Password as secure String
$password = ConvertTo-SecureString "xxxx" -AsPlainText -Force

#Get the Client Context and Bind the Site Collection
$context = New-Object Microsoft.SharePoint.Client.ClientContext($site)

#Authenticate
$credentials = New-Object Microsoft.SharePoint.Client.SharePointOnlineCredentials($admin , $password)
$context.Credentials = $credentials

$list = $context.Web.Lists.GetByTitle('mm')

[string[]]$props = @("DefaultNewFormUrl","DefaultDisplayFormUrl","DefaultEditFormUrl")
$list.Retrieve($props)

$context.Load($list)
$context.ExecuteQuery()


$List.DefaultNewFormUrl
$List.DefaultDisplayFormUrl
$List.DefaultEditFormUrl

#Get all forms
$context.Load($list.forms)
$context.ExecuteQuery()
$list.forms

# check CustomizedPageStatus
$f_form= $list.RootFolder.Files.GetByUrl($list.DefaultDisplayFormUrl)
$context.Load($f_form)
$context.ExecuteQuery()

$f_form.CustomizedPageStatus
