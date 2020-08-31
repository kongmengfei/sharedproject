using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Microsoft.SharePoint.Client.File;

namespace TeamifySharePointClassicSite
{
    class FileFormat
    {
        static void Main(string[] args)
        {
            string SiteUrl = "https://abc.sharepoint.com/sites/s01";

            var pwd = "password";
            var username = "admin@abc.onmicrosoft.com";

            var authManager = new AuthenticationManager();
            ClientContext context = authManager.GetSharePointOnlineAuthenticatedContextTenant(SiteUrl, username, pwd);

            context.Load(context.Site, x => x.ServerRelativeUrl);
            context.ExecuteQuery();

            File fs = context.Site.RootWeb.GetFileByServerRelativeUrl(context.Site.ServerRelativeUrl + "/Shared Documents/Book1.xlsx");
            context.Load(fs);
            context.ExecuteQuery();

            var attInfo = new AttachmentCreationInformation();
            attInfo.FileName = fs.Name;
            var data = fs.OpenBinaryStream();

            ListItem item = context.Web.Lists.GetByTitle("ckkk").GetItemById(2);
            context.Load(item);
            context.ExecuteQuery();


            attInfo.ContentStream = data.Value;
            var att = item.AttachmentFiles.Add(attInfo);
            context.Load(att);
            context.ExecuteQuery();



            Console.ReadKey();
        }
    }
}
