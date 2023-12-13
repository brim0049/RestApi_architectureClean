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
    public class StudentRepository
    : EfRepository<Student>, IStudentRepository
    {
        public StudentRepository(MESolutionContext MESolutionContext) : base(MESolutionContext)
        {
        }

        public Task<Student> GetByEmailAsync(string socialSecurityNumber)
        {
            return _MESolutionContext.Students
              .FirstOrDefaultAsync(u => u.SocialSecurityNumber == socialSecurityNumber);
        }
        public Task<Student> GetByPermanetCodeAsync(string permanentCode)
        {
            return _MESolutionContext.Students
              .FirstOrDefaultAsync(u => u.PermanentCode == permanentCode);
        }
    }
}
