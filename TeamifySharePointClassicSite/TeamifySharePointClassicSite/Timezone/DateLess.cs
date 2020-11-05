using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamifySharePointClassicSite
{
    class DateLess
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
            List mylist = context.Web.Lists.GetByTitle("kkkk");
            ListItem targetitems = mylist.GetItemById(17);

            context.Load(spTimeZone);              
            context.Load(targetitems);         
            context.ExecuteQuery();

            Console.WriteLine(spTimeZone.Description);
            Console.WriteLine(targetitems["mStartDate"] is null);

            var orginaldate = Convert.ToDateTime(targetitems["mStartDate"]);

            string strStartdate = orginaldate.ToLocalTime().ToString();
            Console.WriteLine(strStartdate);

            ClientResult<DateTime> cr = spTimeZone.UTCToLocalTime(orginaldate);
            context.ExecuteQuery();
            Console.WriteLine(cr.Value);

            Console.ReadKey();
        }
    }
}
