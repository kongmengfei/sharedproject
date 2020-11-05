using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamifySharePointClassicSite.Timezone
{
    class John1105John
    {
        static void Main(string[] args)
        {
            var siteurl = "https://isvdevchat.sharepoint.com/sites/sbdev";
            var pwd = "dlmm=920625m";
            var username = "admin@ISVDevChat.onmicrosoft.com";

            var authManager = new AuthenticationManager();
            ClientContext context = authManager.GetSharePointOnlineAuthenticatedContextTenant(siteurl, username, pwd);

            // timezone
            var spTimeZone = context.Web.RegionalSettings.TimeZone;            

            //context.Load(spTimeZone);            
            //context.ExecuteQuery();

            //Console.WriteLine(spTimeZone.Description);


            var orginaldate = DateTime.ParseExact("14/10/2020 13:14:11","dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            
            //Console.WriteLine(orginaldate);

            ClientResult<DateTime> cr = spTimeZone.UTCToLocalTime(orginaldate);
            context.ExecuteQuery();
            Console.WriteLine(cr.Value);

            Console.ReadKey();
        }
    }
}
