using MESolution.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESolution.Core.Specifications
{
    public class RequestByUser : BaseSpecification<FinancialAidApplication>
    {
        public RequestByUser(int userId) : base(x => x.Student.Id == userId)
        {
        }
    }
}
