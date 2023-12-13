using MESolution.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESolution.Infrastructure
{
    public class MESolutionContext: DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<FinancialAidApplication> FinancialAidApplications { get; set; }
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
        public MESolutionContext(DbContextOptions options) : base(options) { }

        public MESolutionContext() : base(new DbContextOptionsBuilder<MESolutionContext>()
                    .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=DBMeSolutionDB;Trusted_Connection=True;")
                    .Options)
        { }
    }
}
