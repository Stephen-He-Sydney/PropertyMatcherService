using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainTest.Domain.Models;

namespace DomainTest.Domain.Interfaces
{
    public interface IPropertyService
    {
        List<Property> GetAgencyPropertyByDefault();

        Property GetDatabasePropertyByAgencyCode(string agencyCode);
    }
}
