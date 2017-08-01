using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.Practices.Unity;
using DomainTest.Domain.Interfaces;
using DomainTest.Domain.Models;
using DomainTest.Business;
using DomainTest.Data;
using DomainTest.Data.Repository;

namespace UBTTest.Business
{
    public class ServiceLayerBootstrapper
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<DbContext, DomainContext>();
            container.RegisterType<IRepository<Property>, EFRepository<Property>>();

            container.RegisterType<IPropertyMatcher, PropertyService>();
            container.RegisterType<IPropertyService, PropertyService>();
        }
    }
}
