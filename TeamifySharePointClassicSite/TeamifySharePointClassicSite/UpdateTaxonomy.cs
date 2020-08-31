using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using OfficeDevPnP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamifySharePointClassicSite
{
    class UpdateTaxonomy
    {
        static void Main(string[] args)
        {
            var siteurl = "https://xxx.sharepoint.com/sites/sbdev";
            var pwd = "****";
            var username = "abc@xxx.onmicrosoft.com";

            var authManager = new AuthenticationManager();
            ClientContext context = authManager.GetSharePointOnlineAuthenticatedContextTenant(siteurl, username, pwd);

            List mylist = context.Web.Lists.GetByTitle("kkkk");
            ListItem targetitems = mylist.GetItemById(10);
            var oField = mylist.Fields.GetByInternalNameOrTitle("MyTaxonomy");

            context.Load(targetitems);
            context.Load(oField);
            context.ExecuteQuery();

            TaxonomyField targetField;
            if (oField.TypeDisplayName == "Managed Metadata")
            {
                // cast field to "TaxonomyField"
                targetField = context.CastTo<TaxonomyField>(oField);
            }
            else
            {
                Console.WriteLine("wrong field type");
                return;
            }

            TaxonomyFieldValueCollection tfvc = new TaxonomyFieldValueCollection(targetitems.Context, null, targetField);
                      
                     
            tfvc.PopulateFromLabelGuidPairs(@"group3|b3705b01-7dd2-47ad-a479-68f1c4dc4071");
            tfvc.PopulateFromLabelGuidPairs(@"group4|611c72f2-f42a-41ad-94b3-f9abfc0f6295");

            targetField.SetFieldValueByValueCollection(targetitems, tfvc);


            targetitems.Update();
            context.ExecuteQuery();


            Console.ReadKey();
        }
    }
}
