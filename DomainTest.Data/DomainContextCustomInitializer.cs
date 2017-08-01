using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTest.Data
{
    public class DomainContextCustomInitializer : IDatabaseInitializer<DomainContext>
    {
        public void InitializeDatabase(DomainContext context)
        {
            if (!context.Database.Exists())
            {
                context.Database.Create();
            }
        }
    }
}
