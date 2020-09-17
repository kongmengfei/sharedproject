/*****************************************************************************
 * PresidentsServiceUnitTests.cs
 * WCF REST service hosted in SharePoint 2013
 * 
 * Copyright (c) Jason Barkes.
 * All rights reserved.
 * http://jbarkes.blogspot.com
 *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
 * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class PresidentsServiceUnitTests
    {
        #region Private Members

        private static string _serviceUrl =
            "http://c4968397007/_vti_bin/BarkesServices/PresidentsService.svc";

        private enum HttpVerb { HttpGet, HttpPost };

        #endregion

        #region WCF Service Helpers

        private string CallService(string serviceUrl,
            HttpVerb verb = HttpVerb.HttpGet, string data = null)
        {
            WebClient client = new WebClient();
            client.UseDefaultCredentials = true;
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            string response = string.Empty;

            switch (verb)
            {
                case HttpVerb.HttpGet:
                    response = client.DownloadString(serviceUrl);
                    break;
                case HttpVerb.HttpPost:
                    response = client.UploadString(serviceUrl, "POST", data);
                    break;
            }

            return response;
        }

        #endregion

        #region Presidents WCF Service Unit Tests

        [TestMethod]
        public void GetAll_UnitTest()
        {
            string response = CallService(_serviceUrl + "/GetAllPresidents");
            List<President> presidents = JsonHelper.Deserialize<List<President>>(response);

            Assert.IsNotNull(response);
            Assert.IsTrue(presidents.Count > 0);
        }

        [TestMethod]
        public void GetByLastName_UnitTest()
        {
            string response = CallService(_serviceUrl + "/GetPresidentsByLastName/" + "bush");
            List<President> presidents = JsonHelper.Deserialize<List<President>>(response);

            Assert.IsNotNull(response);
            Assert.IsTrue(presidents.Count == 2);
        }

        [TestMethod]
        public void GetByLastNameAndFirstName_UnitTest()
        {
            string response = CallService(_serviceUrl + "/GetPresidentsByLastFirstName/" + "truman/" + "harry");
            List<President> presidents = JsonHelper.Deserialize<List<President>>(response);

            Assert.IsNotNull(response);
            Assert.IsTrue(presidents.Count == 1);
        }

        [TestMethod]
        public void GetById_UnitTest()
        {
            string response = CallService(_serviceUrl + "/GetPresidentById/" + "1");
            President president = JsonHelper.Deserialize<President>(response);

            Assert.IsNotNull(response);
            Assert.AreEqual("washington", president.LastName.ToLower());
        }

        [TestMethod]
        public void AddNewPresident_UnitTest()
        {
            President president = new President
            {
                Id = "45",
                FirstName = "Jason",
                LastName = "Barkes",
                EmailAddress = "jbarkes@mail.com"
            };

            string data = JsonHelper.Serialize<President>(president);
            string response = CallService(_serviceUrl + "/AddPresident",
                HttpVerb.HttpPost, data);

            Assert.AreEqual("true", response.ToLower());
        }

        #endregion
    }
}
