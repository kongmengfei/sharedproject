using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp_bkwebTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");
            
            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app!

            //create jwt
            var rawcert = Properties.Resources.MyCompanyName;
            X509Certificate2 certificate = new X509Certificate2(rawcert, "zxc865505218");
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = "https://login.microsoftonline.com/{00000000}/oauth2/v2.0/token",
                Expires = DateTime.UtcNow.AddSeconds(50),
                Issuer = "e0cexxxxxxxxxxx63112",
                NotBefore = DateTime.UtcNow.AddSeconds(-100),
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim("sub", "e0cexxxxxxxxxxx63112"),
                            new Claim("email", "abc@*****.onmicrosoft.com"),
                            new Claim("jti", new Guid().ToString()),
                        }),
                SigningCredentials = new X509SigningCredentials(certificate)               
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var stoken= tokenHandler.CreateToken(securityTokenDescriptor);
            string token = tokenHandler.WriteToken(stoken);

            Console.WriteLine(token);
            Console.ReadKey();
        }
    }
}
