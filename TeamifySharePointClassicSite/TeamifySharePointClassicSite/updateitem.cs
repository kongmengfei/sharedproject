using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;

namespace TeamifySharePointClassicSite
{
    class Updateitem
    {
        static void Main(string[] args)
        {
            string clientId = "e0cxxxxxxxxxx112"; //e.g. 01e54f9a-81bc-4dee-b15d-e661ae13f382
            string clientSecret = @"1kQxxxxxxxx5-qZ";
            string tenantID = "8a4xxxxxxxxxxcfeef";

            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                    .Create(clientId)
                    .WithTenantId(tenantID)
                    .WithClientSecret(clientSecret)
                    .Build();

            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);
            GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            var group = new Group
            {
                Description = "Group with designated owner and members",
                DisplayName = "Operations group",
                GroupTypes = new List<String>()
                {
                    "Unified"
                },
                MailEnabled = true,
                MailNickname = "operations2019",
                SecurityEnabled = false,
                AdditionalData = new Dictionary<string, object>()
                {
                    {
                        "members@odata.bind",new []{ "https://graph.microsoft.com/v1.0/users/942ddxxxxxxxx070a4a", "https://graph.microsoft.com/v1.0/users/cb281exxxxxxxxxxxx4f5255e8" }
                    },
                    {
                        "owners@odata.bind",new []{ "https://graph.microsoft.com/v1.0/users/73d721xxxxxxxxe0a567" }
                    }
                }
            };

            var res = graphClient.Groups.Request().AddAsync(group).Result;
            Console.WriteLine(res);

            Console.ReadKey();
        }
    }
}
