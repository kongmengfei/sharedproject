using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamifySharePointClassicSite
{
    class AddPlanner
    {
        static void Main(string[] args)
        {
            string clientId = "e0cefc2c-1104-4622-81ab-f7b421063112"; //e.g. 01e54f9a-81bc-4dee-b15d-e661ae13f382
            string clientSecret = @"1kQTuQ_/Fb9Z8mkyN@BdIPgv?um05-qZ";
            string tenantID = "8a400d02-3263-4d07-9101-38c6872cfeef";

            IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder
            .Create(clientId)
            .WithRedirectUri("msale0cefc2c-1104-4622-81ab-f7b421063112://auth")
            .Build();

            string[] scopes = new string[] { "Group.ReadWrite.All" };
            InteractiveAuthenticationProvider authProvider = new InteractiveAuthenticationProvider(publicClientApplication, scopes);
                     

            GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            var plans = graphClient.Groups["ea866578-ee7f-48ba-bdbd-9acca12b6da8"].Planner.Plans.Request().GetAsync();

            Console.WriteLine(plans.Result);

            Console.ReadKey();
        }
    }
}
