using Azure.Core;
using MESolution.Core.Entities;
using MESolution.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESolution.Infrastructure
{
    public class FinancialAidApplicationRepository : EfRepository<FinancialAidApplication>, IFinancialAidApplicationRepository
    {
        public FinancialAidApplicationRepository(MESolutionContext MESolutionContext) : base(MESolutionContext)
        {
        }

        public Task<FinancialAidApplication> GetByIdWithRequestItemsAsync(int id)
        {
            return _MESolutionContext.FinancialAidApplications
              .Include(r => r.Student)
              .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
