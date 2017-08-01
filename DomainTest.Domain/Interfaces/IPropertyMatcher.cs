using DomainTest.Domain.Models;

namespace DomainTest.Domain.Interfaces
{
    public interface IPropertyMatcher
    {
        bool IsMatch(Property agencyProperty, Property databaseProperty);
    }
}
