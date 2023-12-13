using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESolution.Infrastructure
{
    public class MESolutionContextContextFactory : IDesignTimeDbContextFactory<MESolutionContext>
    {
        public MESolutionContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MESolutionContext>();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=DBMeSolutionDB;Trusted_Connection=True;");

            return new MESolutionContext(optionsBuilder.Options);
        }
    }
}
