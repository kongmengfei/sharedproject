#Load SharePoint CSOM Assemblies
Add-Type -Path "C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\16\ISAPI\Microsoft.SharePoint.Client.dll"
Add-Type -Path "C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\16\ISAPI\Microsoft.SharePoint.Client.Runtime.dll"

#Admin User Principal Name
$username = 'admin@ISVDevChat.onmicrosoft.com'

#Get Password as secure String
$password = ConvertTo-SecureString "dlmm=920625m" -AsPlainText -Force

#Authenticate
$credentials = New-Object Microsoft.SharePoint.Client.SharePointOnlineCredentials($username , $password)
  
#Function to Move a File
Function Move-SPOFile([String]$SiteURL, [String]$SourceFileURL, [String]$TargetFileURL)
{
    Try{
        #Setup the context
        $Ctx = New-Object Microsoft.SharePoint.Client.ClientContext($SiteURL)
        $Ctx.Credentials = $credentials
      
        #sharepoint online powershell to copy files
        $MoveCopyOpt = New-Object Microsoft.SharePoint.Client.MoveCopyOptions
        $MoveCopyOpt.KeepBoth= $true  

        $Overwrite = $true

        [Microsoft.SharePoint.Client.MoveCopyUtil]::CopyFile($Ctx, $SourceFileURL, $TargetFileURL, $Overwrite, $MoveCopyOpt)
        $Ctx.ExecuteQuery()
  
        Write-host -f Green "File Moved Successfully!"

    }
    Catch {
        write-host -f Red "Error Moving the File!" $_.Exception.Message
    }
}
  
#Set Config Parameters
$SiteURL="https://isvdevchat.sharepoint.com/sites/sbdev"
$SourceFileURL="https://isvdevchat.sharepoint.com/sites/sbdev/My%20test%20doc%20lib/Pictures/200MB.zip"
$TargetFileURL="https://isvdevchat.sharepoint.com/sites/sbdev/My%20test%20doc%20lib/SDK/200MB.zip"
  

#Call the function to Move the File
Move-SPOFile $SiteURL $SourceFileURL $TargetFileURL


