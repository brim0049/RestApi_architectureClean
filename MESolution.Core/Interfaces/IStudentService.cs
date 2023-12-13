using MESolution.Core.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESolution.Core.Interfaces
{
    public interface IStudentService
    {
        Task<Student> GetStudentById(int id);

        Task<Student> GetStudentByEmail(string SocialSecurityNumber);
        Task<Student> RegisterStudent(Student Student);

        Task UpdateStudent(Student Student);
        Task PatchStudent(int id, JsonPatchDocument<Student> patchDocument);
       Task<Student> AuthenticateStudent(string PermanentCode, string password);

    }
}
