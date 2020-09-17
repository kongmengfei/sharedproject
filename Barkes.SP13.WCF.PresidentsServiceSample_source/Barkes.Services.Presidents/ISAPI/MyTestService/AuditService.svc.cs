/*****************************************************************************
 * PresidentsService.svc.cs
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

using Microsoft.SharePoint.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using Barkes.Services.Presidents.Model;
using Microsoft.SharePoint;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;

namespace Barkes.Services.Presidents
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class AuditService : IAuditService
    {
        
        public List<Aduit> GetAuditEntries()
        {
            SPSite site = new SPSite("http://sp2016");
            SPAudit audit = site.Audit;
            SPAuditEntryCollection collection = audit.GetEntries();

            List<Aduit> Data = new List<Aduit>();

            foreach (SPAuditEntry i in collection)
            {
                Data.Add(TransExpV2<SPAuditEntry, Aduit>.Trans(i));
            }

            return Data;
        }
    }
}