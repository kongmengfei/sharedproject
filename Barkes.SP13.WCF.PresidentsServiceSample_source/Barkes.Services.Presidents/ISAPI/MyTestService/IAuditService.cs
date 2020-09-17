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
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using Barkes.Services.Presidents.Model;
using Microsoft.SharePoint;

namespace Barkes.Services.Presidents
{
    [ServiceContract]
    interface IAuditService
    {
        [OperationContract]
        [WebGet(UriTemplate = "GetAuditEntries",ResponseFormat = WebMessageFormat.Json)]
        List<Aduit> GetAuditEntries();
    }
}
