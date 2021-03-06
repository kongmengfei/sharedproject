﻿using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamifySharePointClassicSite.msgraph
{
    class GraphUserinfoList
    {
        static void Main(string[] args)
        {
            string clientId = "e0cefc2c-1104-4622-81ab-f7bxxx3112"; //e.g. 01e54f9a-81bc-4dee-b15d-e661ae13f382
            
            IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder
            .Create(clientId)
            .WithRedirectUri("msale0cefc2c-1104-4622-81ab-f7b421063112://auth")
            .Build();

            string[] scopes = new string[] { "Sites.ReadWrite.All" };
            InteractiveAuthenticationProvider authProvider = new InteractiveAuthenticationProvider(publicClientApplication, scopes);

            GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            var queryOptions = new List<QueryOption>()
            {
                new QueryOption("expand", "fields")
            };

            var items = graphClient.Sites["abc.sharepoint.com,8f9005da-df16-4c30-9b91-4980e1e40d31,af165982-3b91-4d97-a12d-2a47800cc52d"]
                .Lists["User Information List"].Items
                .Request(queryOptions)
                .GetAsync();

            Console.WriteLine(items.Result);

            Console.ReadKey();
        }
    }
}
