using System.Collections.Generic;

namespace DomainTest.Domain.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string AgencyCode { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }

    public class PropertyList
    {
        public List<Property> Property { get; set; }
    }
}
