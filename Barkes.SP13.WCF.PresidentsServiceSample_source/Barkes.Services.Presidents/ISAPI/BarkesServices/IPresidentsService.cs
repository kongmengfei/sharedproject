/*****************************************************************************
 * IPresidentsService.cs
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
using System.ServiceModel;
using System.ServiceModel.Web;
using Barkes.Services.Presidents.Model;

namespace Barkes.Services.Presidents
{
    [ServiceContract]
    interface IPresidentsService
    {
        [OperationContract]
        [WebGet(UriTemplate = "GetAllPresidents",
            ResponseFormat = WebMessageFormat.Json)]
        List<President> GetAllPresidents();

        [OperationContract (Name="GetPresidentsByLastName")]
        [WebGet(UriTemplate = "GetPresidentsByLastName/{lastName}",
            ResponseFormat = WebMessageFormat.Json)]
        List<President> GetPresidentsByName(string lastName);

        [OperationContract (Name="GetPresidentsByLastFirstName")]
        [WebGet(UriTemplate = "GetPresidentsByLastFirstName/{lastName}/{firstName}",
            ResponseFormat = WebMessageFormat.Json)]
        List<President> GetPresidentsByName(string lastName, string firstName);

        [OperationContract]
        [WebGet(UriTemplate = "GetPresidentById/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        President GetPresidentById(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddPresident",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        bool AddPresident(President president);
    }
}
