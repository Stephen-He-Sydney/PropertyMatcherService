namespace DomainTest.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DomainTest.Domain.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<DomainTest.Data.DomainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DomainTest.Data.DomainContext context)
        {
            // Don't insert seed data, when db is existed and target table has got data
            if (!context.Database.Exists()
                || !context.Properties.Any())
            {
                var properties = new List<Property>
                {
                    new Property()
                    {
                        Id = 1,
                        AgencyCode = "OTBRE",
                        Name = "Super High Apartments, Sydney",
                        Address = "32 Sir John Young Crescent, Sydney NSW"
                    },
                    new Property()
                    {
                        Id = 2,
                        AgencyCode = "LRE",
                        Longitude = 22,
                        Latitude = 33
                    },
                    new Property()
                    {
                        Id = 3,
                        AgencyCode = "CRE",
                        Name = "The Summit Apartments"
                    }
                };

                properties.ForEach(x => context.Properties.AddOrUpdate(x));
                context.SaveChanges();
            }
        }
    }
}
