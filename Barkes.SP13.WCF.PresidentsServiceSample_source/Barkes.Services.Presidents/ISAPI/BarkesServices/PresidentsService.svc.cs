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

namespace Barkes.Services.Presidents
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class PresidentsService : IPresidentsService
    {
        #region Private Members

        private List<President> _presidents;
        private List<President> Presidents
        {
            get
            {
                // If there aren't any presidents in our list, populate with samples
                _presidents = _presidents ?? new List<President>(SampleData.SamplePresidents);
                return _presidents;
            }
        }

        #endregion

        #region IPresidentsService Implementation

        public List<President> GetAllPresidents()
        {
            return Presidents;
        }

        public List<President> GetPresidentsByName(string lastName)
        {
            return GetPresidentsByName(lastName, string.Empty);
        }

        public List<President> GetPresidentsByName(string lastName, string firstName)
        {
            var query = from President p in Presidents
                        where p.LastName.ToLower().Contains(lastName.ToLower())
                           && (string.IsNullOrWhiteSpace(firstName) 
                                ? true 
                                : p.FirstName.ToLower().Contains(firstName.ToLower()))
                        select p;

            return query.ToList();
        }

        public President GetPresidentById(string id)
        {
            var query = from President p in Presidents
                        where p.Id == id
                        select p;

            return query.FirstOrDefault();
        }

        public bool AddPresident(President president)
        {
            Presidents.Add(president);
            return true;
        }

        #endregion

    }
}