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
    /// A property is considered a match if the names match when the words in the name of the property are reversed.
    /// </summary>
    public class CREMatcher : IPropertyMatcher
    {
        private const string AgencyCode = "CRE";

        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            return agencyProperty.AgencyCode.ToLower().Trim() == AgencyCode.ToLower().Trim()
                   && StringHelper.RemoveWhitespace(StringHelper.ReverseWordSequence(agencyProperty.Name).Trim()) == StringHelper.RemoveWhitespace(databaseProperty.Name.Trim());
        }
    }
}
