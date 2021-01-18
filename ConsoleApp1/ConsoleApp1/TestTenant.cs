using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    class TestTenant
    {
        static void Main(string[] args)
        {
            ClientContext ctx = new ClientContext("https://isvdevchat-admin.sharepoint.com");
            ctx.ExecutingWebRequest += Ctx_ExecutingWebRequest;


            var tenant = new Tenant(ctx);
            var siteProperties = tenant.GetSitePropertiesByUrl("https://isvdevchat.sharepoint.com/sites/sbdev", true);
            ctx.Load(siteProperties);
            ctx.ExecuteQuery();

            Console.WriteLine(siteProperties);
        }

        static void Ctx_ExecutingWebRequest(object sender, WebRequestEventArgs e)
        {
            var token = @"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IjVPZjlQNUY5Z0NDd0NtRjJCT0hIeEREUS1EayIsImtpZCI6IjVPZjlQNUY5Z0NDd0NtRjJCT0hIeEREUS1EayJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTBmZjEtY2UwMC0wMDAwMDAwMDAwMDAvaXN2ZGV2Y2hhdC1hZG1pbi5zaGFyZXBvaW50LmNvbUA4YTQwMGQwMi0zMjYzLTRkMDctOTEwMS0zOGM2ODcyY2ZlZWYiLCJpc3MiOiIwMDAwMDAwMS0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDBAOGE0MDBkMDItMzI2My00ZDA3LTkxMDEtMzhjNjg3MmNmZWVmIiwiaWF0IjoxNjA4NzkzMTA0LCJuYmYiOjE2MDg3OTMxMDQsImV4cCI6MTYwODg3OTgwNCwiaWRlbnRpdHlwcm92aWRlciI6IjAwMDAwMDAxLTAwMDAtMDAwMC1jMDAwLTAwMDAwMDAwMDAwMEA4YTQwMGQwMi0zMjYzLTRkMDctOTEwMS0zOGM2ODcyY2ZlZWYiLCJuYW1laWQiOiJiMjE3OWEyMC1hMTUzLTRjMzEtYTFlZS1lZjkxN2JiNDVhZmJAOGE0MDBkMDItMzI2My00ZDA3LTkxMDEtMzhjNjg3MmNmZWVmIiwib2lkIjoiZTE2NGQzZDItZWI4OC00NWI5LWJmMTEtNjcxOTE4OTc0MGZlIiwic3ViIjoiZTE2NGQzZDItZWI4OC00NWI5LWJmMTEtNjcxOTE4OTc0MGZlIiwidHJ1c3RlZGZvcmRlbGVnYXRpb24iOiJmYWxzZSJ9.O0rUWbCzbtyW0j560fa2yXNWoEZLSca6O7gplhVc_-Y0p3pjm8yRMCTvIfJvpFlTU5p20yvVxWHIBDCAuNABYYYFn_xQjHbOV-ZGousMaefdsC22mYCfWJDNbBvlIC24aAoLCb1cNNj6dUSVfxQ5DfhkHW-NuapMoWSX_Tm3VJvna5AmyEdeOe5DRE2hpJ4niALoTNlWYfasEda1utc9Tpzzslv0WdgUsiYrCYwe0274pV0Pn43T4csrHqOYfiweTt3-VO7ZQBvAV8001WcjodxE5tDKY3G3R6A02FD0WIO7GciiVhzE75Is0uHlajHqhv5Y4HhqWg_Y0xjv6mvk6A";
            e.WebRequestExecutor.RequestHeaders["Authorization"] = "Bearer " + token;
        }
    }
}
