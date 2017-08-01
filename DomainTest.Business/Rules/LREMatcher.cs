using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainTest.Domain.Interfaces;
using DomainTest.Domain.Models;
using DomainTest.Domain.Utilities;

namespace DomainTest.Business.Rules
{
    /// <summary>
    /// A property is considered to be a match if the agency code is the same and the property is within 200 metres or less of the actual property location.
    /// </summary>
    public class LREMatcher : IPropertyMatcher
    {
        private const string AgencyCode = "LRE";

        // Set max distance as 200 metres
        private const int MaxDistance = 200;

        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            return agencyProperty.AgencyCode.ToLower().Trim() == AgencyCode.ToLower().Trim()
                   && MapHelper.GetDistanceBetween(agencyProperty.Latitude, agencyProperty.Longitude, databaseProperty.Latitude, databaseProperty.Longitude) <= MaxDistance;
        }
    }
}
