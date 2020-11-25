using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace TeamifySharePointClassicSite.CsRest
{
    class RestCs
    {
        static void Main(string[] args)
        {
            var siteurl = "https://isvdevchat.sharepoint.com";
            var pwd = "dlmm=920625m";
            var userName = "admin@ISVDevChat.onmicrosoft.com";

            SecureString securePassword = new SecureString();
            pwd.ToCharArray().ToList().ForEach(s => securePassword.AppendChar(s));

            var credentials = new SharePointOnlineCredentials(userName, securePassword);

            HttpWebRequest endpointRequest = (HttpWebRequest)WebRequest.Create($"{siteurl}/_api/web/lists");
            endpointRequest.Credentials = credentials;
            endpointRequest.Headers.Add("X-FORMS_BASED_AUTH_ACCEPTED", "f");
            endpointRequest.Method = "GET";
            endpointRequest.Accept = "application/json;odata=verbose";
           
            HttpWebResponse endpointResponse = (HttpWebResponse)endpointRequest.GetResponse();
            StreamReader streamR = new StreamReader(endpointResponse.GetResponseStream());

            Console.WriteLine(streamR.ReadToEnd());

            Console.ReadKey();
        }
    }
}
