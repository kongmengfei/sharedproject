using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamifySharePointClassicSite.csom
{
    class UpdateLastModify
    {
        static void Main(string[] args)
        {
            string SiteUrl = "https://isvdevchat.sharepoint.com/sites/sbdev";

            var pwd = "dlmm=920625m";
            var username = "admin@ISVDevChat.onmicrosoft.com";

            var authManager = new AuthenticationManager();
            ClientContext context = authManager.GetSharePointOnlineAuthenticatedContextTenant(SiteUrl, username, pwd);

            var documents = context.Web.Lists.GetByTitle("kkkk");
            Field oField = documents.Fields.GetByInternalNameOrTitle("Last_x0020_Modified");
            
            context.Load(oField);

            oField.ReadOnlyField = false;
            documents.Update();
            
            context.ExecuteQuery();

            // get item and set its value
            var Item = documents.GetItemById(15);

            context.Load(Item, i => i["Last_x0020_Modified"]);
            context.ExecuteQuery();

            Console.WriteLine(Item["Last_x0020_Modified"]);

            Item["Last_x0020_Modified"] = "2020-09-07T04:30:35Z";

            Item.Update();
            context.ExecuteQuery();


            //
            oField.ReadOnlyField = true;
            oField.Update();

            context.ExecuteQuery();

            Console.ReadKey();

        }
    }
}
