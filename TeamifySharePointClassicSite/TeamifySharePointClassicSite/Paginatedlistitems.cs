using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamifySharePointClassicSite
{
    class Paginatedlistitems
    {
        static void Main(string[] args)
        {
            string clientId = "e0cexxxxx1063112"; //e.g. 01e54f9a-81bc-4dee-b15d-e661ae13f382
            string clientSecret = @"1kQxxxxxxxm05-qZ";
            string tenantID = "8a400xxxx872cfeef";

            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithTenantId(tenantID)
                .WithClientSecret(clientSecret)
                .Build();

            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);
            GraphServiceClient graphClient = new GraphServiceClient(authProvider);


            var totalitems = new List<Microsoft.Graph.ListItem>();


            var items = graphClient
                .Sites["xxxx.sharepoint.com,5ae4xxxxx20ad27b850b,352d1542-3xxx41552082"]
                .Lists["40418cxxxxe9f0129"].Items
                .Request()
                .GetAsync().Result;

            totalitems.AddRange(items.CurrentPage);
            while (items.NextPageRequest != null)
            {
                items = items.NextPageRequest.GetAsync().Result;
                totalitems.AddRange(items.CurrentPage);
            }

            Console.WriteLine(totalitems);
            Console.ReadKey();
        }
    }
}
