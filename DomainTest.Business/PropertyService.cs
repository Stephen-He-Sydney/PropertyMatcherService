using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;
using DomainTest.Domain.Interfaces;
using DomainTest.Domain.Models;
using DomainTest.Business.Rules;
using DomainTest.Business.Config;

namespace DomainTest.Business
{
    /// <summary>
    /// Match real estate based on different rules
    /// </summary>
    public class PropertyService : IPropertyMatcher, IPropertyService
    {
        private readonly List<IPropertyMatcher> _matcherList;
        private readonly IRepository<Property> _propertyRepository;

        /// <summary>
        /// Register matcher rules for service
        /// </summary>
        public PropertyService(IRepository<Property> propertyRepository)
        {
            _propertyRepository = propertyRepository;

            _matcherList = new List<IPropertyMatcher>();

            // We can add more new rules in future
            _matcherList.Add(new OTBREMatcher());
            _matcherList.Add(new LREMatcher());
            _matcherList.Add(new CREMatcher());
        }

        /// <summary>
        /// Match input property with all available matcher rules
        /// </summary>
        /// <param name="agencyProperty"></param>
        /// <param name="databaseProperty"></param>
        /// <returns></returns>
        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            foreach (var aMatcher in _matcherList)
            {
                if (aMatcher.IsMatch(agencyProperty, databaseProperty)) return true;
            }

            return false;
        }

        /// <summary>
        /// Read all first default agency property data based on different agency code
        /// </summary>
        /// <returns></returns>
        public List<Property> GetAgencyPropertyByDefault()
        {
            if (!File.Exists(FileDataConfig.ClientSource))
            {
                throw new FileNotFoundException($"file not found: {FileDataConfig.ClientSource}", Path.GetFileName(FileDataConfig.ClientSource));
            }
            else
            {
                var list = new PropertyList();
                FileStream fs = null;
                BufferedStream bs = null;
                StreamReader sr = null;

                try
                {
                    using (fs = File.Open(FileDataConfig.ClientSource, FileMode.Open, FileAccess.Read))
                    using (bs = new BufferedStream(fs))
                    using (sr = new StreamReader(bs))
                    {
                        string jsonString = sr.ReadToEnd();
                        list = new JavaScriptSerializer().Deserialize<PropertyList>(jsonString);

                        var propertyList = new List<Property>();
                        var agencyCodes = list.Property.Select(x => x.AgencyCode).Distinct();
                        foreach (var aCode in agencyCodes)
                        {
                            propertyList.Add(list.Property.FirstOrDefault(x => string.Equals(x.AgencyCode.Trim(), aCode.Trim(), StringComparison.OrdinalIgnoreCase)));
                        }

                        return propertyList;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    sr?.Dispose();
                    bs?.Dispose();
                    fs?.Dispose();
                }
            }
        }

        public Property GetDatabasePropertyByAgencyCode(string agencyCode)
        {
            var result = _propertyRepository.Table
                                            .FirstOrDefault(x => x.AgencyCode.Trim() == agencyCode.Trim());
            return result;
        }
    }
}
