using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;

namespace FunctionApp2
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            string SiteUrl = "https://abc.sharepoint.com/sites/testprivate";

            var pwd = "xxxx";
            var username = "admin@abc.onmicrosoft.com";

            var authManager = new AuthenticationManager();
            ClientContext context = authManager.GetSharePointOnlineAuthenticatedContextTenant(SiteUrl, username, pwd);

            var oweb = context.Web;

            context.Load(oweb);
            context.ExecuteQuery();


            log.Info($"C# Site: {oweb.Title}");

        }
    }
}
