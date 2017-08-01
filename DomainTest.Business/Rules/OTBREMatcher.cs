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
    /// A property is considered to be a match if both the property name and address match when punctuation is excluded.  
    /// </summary>
    public class OTBREMatcher : IPropertyMatcher
    {
        private const string AgencyCode = "OTBRE";

        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            return agencyProperty.AgencyCode.ToLower().Trim() == AgencyCode.ToLower().Trim()
                   && StringHelper.RemovePunctuation(agencyProperty.Address).ToLower().Trim() == StringHelper.RemovePunctuation(databaseProperty.Address).ToLower().Trim()
                   && StringHelper.RemovePunctuation(agencyProperty.Name).ToLower().Trim() == StringHelper.RemovePunctuation(databaseProperty.Name).ToLower().Trim();
        }
    }
}
