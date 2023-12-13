using MESolution.Core.Entities;
using MESolution.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESolution.Core.Interfaces
{
    public interface IStudentRepository : IAsyncRepository<Student>
    {
        Task<Student> GetByEmailAsync(string SocialSecurityNumber);
        Task<Student> GetByPermanetCodeAsync(string permanetCode); 

    }
}
