using System;
using System.Security;

namespace ConsoleAppNetCore
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Uri site = new Uri("https://abc.sharepoint.com/sites/s01");
            string user = "admin@abc.onmicrosoft.com";
            string rawPassword = "xxxxxx";
            SecureString password = new SecureString();
            foreach (char c in rawPassword) password.AppendChar(c);

            using var authenticationManager = new AuthenticationManager();
            using var context = authenticationManager.GetContext(site, user, password);

            context.Load(context.Web, p => p.Title);
            await context.ExecuteQueryAsync();
            Console.WriteLine($"Title: {context.Web.Title}");
        }
    }
}
