using Microsoft.SharePoint.Client;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml;

namespace ConsoleAppCore
{
    class Program
    {
        private static readonly string siteurl = "https://abc.sharepoint.com/sites/s01";
        private static readonly string username = "admin@abc.onmicrosoft.com";
        private static readonly string password = "xxxxxx";

        static void Main(string[] args)
        {

            //getCookieContainer();

            Console.ReadKey();
        }

        

        private static CookieContainer getCookieContainer()
        {
            var cookiecontainer = new CookieContainer();
            var handler = new HttpClientHandler()
            {
                CookieContainer = cookiecontainer
            };
            HttpClient hclient = new HttpClient(handler);

            var contentxml = new XmlDocument();
            contentxml.LoadXml(Resource1.bk);

            XmlNode usernamenode = contentxml.GetElementsByTagName("Username", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")[0];
            usernamenode.InnerText = username;

            XmlNode passwdnode = contentxml.GetElementsByTagName("Password", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")[0];
            passwdnode.InnerText = password;

            XmlNode endpoint = contentxml.GetElementsByTagName("EndpointReference", "http://www.w3.org/2005/08/addressing")[0].FirstChild;
            endpoint.InnerText = new Uri(siteurl).GetLeftPart(UriPartial.Authority);

            var content = contentxml.OuterXml;

            var httpcontent = new StringContent(content, Encoding.UTF8, "application/xml");
            var step1url = "https://login.microsoftonline.com/extSTS.srf";
            HttpResponseMessage res1 = hclient.PostAsync(step1url, httpcontent).Result;

            var res1con = res1.Content.ReadAsStringAsync().Result;
            Console.WriteLine(res1con);

            XmlDocument resxml = new XmlDocument();
            resxml.LoadXml(res1con);
            XmlNode securitytoken = resxml.GetElementsByTagName("BinarySecurityToken", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")[0];
            string token = securitytoken.InnerText;

            Console.WriteLine("the security token is {0}", token);


            //step2

            string step2url = $"{new Uri(siteurl).GetLeftPart(UriPartial.Authority)}/_forms/default.aspx?wa=wsignin1.0";
            HttpContent hcontent2 = new StringContent(token, Encoding.UTF8, "text/plain");
            HttpResponseMessage res2 = hclient.PostAsync(step2url, hcontent2).Result;

            Console.WriteLine($"step2 result is {res2}{Environment.NewLine}------{Environment.NewLine}");

            //step3
            string step3url = $"{siteurl}/_api/web/currentuser";
            var res = hclient.GetAsync(step3url).Result;

            Console.WriteLine(res.Content.ReadAsStringAsync().Result);

            return cookiecontainer;
        }
    }
}
