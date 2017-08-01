using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DomainTest.Domain.Models;

namespace DomainTest.Data
{
    public class DomainContext : DbContext
    {
        public DomainContext()
            : base("name=DomainContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Database.SetInitializer(new DomainContextCustomInitializer());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
