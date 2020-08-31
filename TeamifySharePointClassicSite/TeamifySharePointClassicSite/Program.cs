using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace TeamifySharePointClassicSite
{
    class Program
    {
        static void Main(string[] args)
        {
            string ClassSiteUrl = "https://xxxx.sharepoint.com/sites/Customsitecol";
            string AdminSiteUrl = "https://xxxx-admin.sharepoint.com";

            string clientId = "e0cxxxxxxxxxxxxx63112"; //e.g. 01e54f9a-81bc-4dee-b15d-e661ae13f382
            string clientSecret = @"AHk8xxxxZIa88QgM7";
            string tenantID = "8a400xxxxxxxxxxxxxxxx872cfeef";

            var pwd = "xxxxxx";
            var username = "xxxx@yyyyy.onmicrosoft.com";

            var authManager = new AuthenticationManager();
            ClientContext context = authManager.GetSharePointOnlineAuthenticatedContextTenant(AdminSiteUrl, username, pwd);

            Tenant tenant = new Tenant(context);
            tenant.CreateGroupForSite(ClassSiteUrl, "display-name-for-group", "alias-for-group", true, null);
            context.ExecuteQuery();

            //get group id
            ClientContext classicalsitectx = authManager.GetSharePointOnlineAuthenticatedContextTenant(ClassSiteUrl, username, pwd);         
            classicalsitectx.Load(classicalsitectx.Site);
            classicalsitectx.ExecuteQuery();
            var groupid = classicalsitectx.Site.GroupId.ToString(); //ea866578-ee7f-48ba-bdbd-9acca12b6da8

            // Link group to a team
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithTenantId(tenantID)
                .WithClientSecret(clientSecret)
                .Build();

            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);
            GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            var team = new Team
            {
                MemberSettings = new TeamMemberSettings
                {
                    AllowCreateUpdateChannels = true,
                    ODataType = null
                },
                MessagingSettings = new TeamMessagingSettings
                {
                    AllowUserEditMessages = true,
                    AllowUserDeleteMessages = true,
                    ODataType = null
                },
                FunSettings = new TeamFunSettings
                {
                    AllowGiphy = true,
                    GiphyContentRating = GiphyRatingType.Strict,
                    ODataType = null
                },
                ODataType = null
            };

            var res=graphClient.Groups[groupid].Team.Request().PutAsync(team).Result;
            Console.WriteLine(res);
            Console.ReadKey();
        }
    }
}
