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
    public class DemandeRepository : EfRepository<FinancialAidApplication>, IDemandeRepository
    { 
        public DemandeRepository(MESolutionContext mESolutionContext) : base(mESolutionContext)
        {
        }

        public Task<FinancialAidApplication> GetByIdWithRequestItemsAsync(int id)
        {
            return _MESolutionContext.FinancialAidApplications
              .Include(r => r.FinancialTransactions)
              .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
