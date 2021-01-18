using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {           
            string SiteUrl = "https://isvdevchat.sharepoint.com/sites/sbdev";

            //var pwd = "dlmm=920625m";
            var pwd = "gnckrrjmdwvbywtr";
            var username = "support@ISVDevChat.onmicrosoft.com";

            Console.WriteLine("Hello World!");

            ClientContext context = new ClientContext(SiteUrl);

            SecureString securestring = new SecureString();
            pwd.ToCharArray().ToList().ForEach(s => securestring.AppendChar(s));

            context.Credentials = new SharePointOnlineCredentials(username, securestring);
            

            var web = context.Web;
            context.Load(web);
            context.ExecuteQuery();

            Console.WriteLine($"web title: {web.Title}");
            
            Console.ReadKey();         
        }
    }
}
