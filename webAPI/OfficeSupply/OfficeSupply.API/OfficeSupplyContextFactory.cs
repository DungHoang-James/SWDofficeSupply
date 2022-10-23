using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OfficeSupply.Data.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSupply.API
{
    public class OfficeSupplyContextFactory : IDesignTimeDbContextFactory<OfficeSupplyDB>
    {
        public OfficeSupplyDB CreateDbContext(string[] args)
        {
            string connectionString = "Server = SWD\\SQLEXPRESS; Database = OfficeSupply; User Id = sa; Password = 123456;";
            var optionsBuilder = new DbContextOptionsBuilder<OfficeSupplyDB>();
            optionsBuilder.UseSqlServer(connectionString);

            return new OfficeSupplyDB(optionsBuilder.Options);
        }
    }
}
