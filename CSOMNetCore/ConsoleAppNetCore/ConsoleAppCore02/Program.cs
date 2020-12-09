using Microsoft.SharePoint.Client;
using System;
using Wictor.Office365;

namespace ConsoleAppCore02
{
    class Program
    {
        static void Main(string[] args)
        {
            var siteurl = "https://isvdevchat.sharepoint.com/sites/sbdev";
            var username = "admin@ISVDevChat.onmicrosoft.com";
            var password = "dlmm=920625m";

            MsOnlineClaimsHelper claimsHelper = new MsOnlineClaimsHelper(siteurl, username, password);
            using (ClientContext context = new ClientContext(siteurl))
            {
                context.ExecutingWebRequest += claimsHelper.clientContext_ExecutingWebRequest;

                context.Load(context.Web);

                context.ExecuteQuery();

                Console.WriteLine("Name of the web is: " + context.Web.Title);

            }

            Console.ReadKey();
        }
    }
}
