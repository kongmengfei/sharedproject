using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamifySharePointClassicSite
{
    class DownloadfileGraph
    {
        static void Main(string[] args)
        {
            string clientId = "e0cefc2xxxxxxxxxxxx1063112";
            string clientSecret = @"1kQxxxxxxxxxxxxxm05-qZ";
            string tenantID = "8a400dxxxxxxxxxxxxxxxxc6872cfeef";

            var host = "abc.sharepoint.com";

            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithTenantId(tenantID)
                .WithClientSecret(clientSecret)
                .Build();

            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);
            GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            var siteid = graphClient.Sites[host].Request().GetAsync().Result.Id;
            Console.WriteLine(siteid);

            ISiteDrivesCollectionPage drives = graphClient.Sites[host].Drives.Request().GetAsync().Result;

            Drive driveIT = drives.CurrentPage.Where(d => d.DriveType == "documentLibrary" && d.Name == "IT").FirstOrDefault();
            DriveItem testfile = graphClient.Sites[host].Drives[driveIT.Id].Root.ItemWithPath("test.txt").Request().GetAsync().Result;

            // this cause the error
            Stream streamtest = graphClient.Sites[siteid]
                            .Drives[driveIT.Id].Root.ItemWithPath("test.txt").Content
                            .Request()
                            .GetAsync().Result;

            StreamReader sr = new StreamReader(streamtest);

            Console.WriteLine(sr.ReadToEnd());



            Console.ReadKey();
        }
    }
}
