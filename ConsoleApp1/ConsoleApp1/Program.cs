using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string SiteUrl = "http://demoaam.contoso2016.com/sites/yyy";

            var pwd = "xxxxx";
            var username = "admin@contoso2016.com";

            Console.WriteLine("Hello World!");

            ClientContext context = new ClientContext(SiteUrl)
            {
                Credentials = new System.Net.NetworkCredential(username, pwd)
            };

            var web = context.Web;
            context.Load(web);
            context.ExecuteQuery();

            Console.WriteLine($"web title: {web.Title}");

            string sourceFile = "http://demoaam.contoso2016.com/sites/yyy/Shared%20Documents/custom.css";
            string destFile = "http://demoaam.contoso2016.com/sites/yyy/Shared%20Documents/test/custom.css";

            MoveCopyUtil.CopyFile(context, sourceFile, destFile,false, new MoveCopyOptions
            {
                KeepBoth= true
            });

            context.ExecuteQuery();

            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
