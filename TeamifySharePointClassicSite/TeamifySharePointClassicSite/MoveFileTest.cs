using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamifySharePointClassicSite
{
    class MoveFileTest
    {
        static void Main(string[] args)
        {
            string SiteUrl = "https://xxx.sharepoint.com/sites/testprivate";     
          
            var pwd = "****";
            var username = "abc@xxx.onmicrosoft.com";

            var authManager = new AuthenticationManager();
            ClientContext context = authManager.GetSharePointOnlineAuthenticatedContextTenant(SiteUrl, username, pwd);

            string sourceFolder = "https://isvdevchat.sharepoint.com/sites/sbdev/My test doc lib/test";
            string destFolder = "https://isvdevchat.sharepoint.com/sites/testprivate/Shared Documents/test";

            MoveCopyUtil.MoveFolder(context, sourceFolder, destFolder,new MoveCopyOptions { 
                KeepBoth=false,
                RetainEditorAndModifiedOnMove=false
            });
            context.ExecuteQuery();
            
            Console.ReadKey();
        }
    }
}
