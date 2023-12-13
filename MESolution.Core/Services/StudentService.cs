using MESolution.Core.Entities;
using MESolution.Core.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESolution.Core.Services
{
    public class StudentService : IStudentService
    {
        public static string GenerateCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // Lettres possibles
            const string digits = "0123456789"; // Chiffres possibles
            StringBuilder codeBuilder = new StringBuilder();
            Random random = new Random();
            // Génération des 4 lettres
            for (int i = 0; i < 4; i++)
            {
                codeBuilder.Append(chars[random.Next(chars.Length)]);
            }

            // Génération des 8 chiffres
            for (int i = 0; i < 8; i++)
            {
                codeBuilder.Append(digits[random.Next(digits.Length)]);
            }

            return codeBuilder.ToString();
        }
        public static string GeneratePassword(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+";

            StringBuilder passwordBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                passwordBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return passwordBuilder.ToString();
        }
        private readonly IStudentRepository _StudentRepository;
        public StudentService(IStudentRepository StudentRepository)
        {
            _StudentRepository = StudentRepository;
        }

        public async Task<Student> GetStudentById(int id)
        {
            
            return await _StudentRepository.GetByIdAsync(id);
        }

        public async Task<Student> RegisterStudent(Student Student)
        {
            Student.PermanentCode = GenerateCode();
            Student.Password = GeneratePassword(12);

            return await _StudentRepository.AddAsync(Student);
        }

        public async Task UpdateStudent(Student Student)
        {
            await _StudentRepository.UpdateAsync(Student);
        }
        public async Task PatchStudent(int id, JsonPatchDocument<Student> patchDocument)
        {
            await _StudentRepository.PatchAsync(id, patchDocument);
        }
        public async Task<Student> GetStudentByEmail(string SocialSecurityNumber)
        {
            return await _StudentRepository.GetByEmailAsync(SocialSecurityNumber);
        }
        public async Task<Student> AuthenticateStudent(string PermanentCode, string password)
        {
            Student Student = await _StudentRepository.GetByPermanetCodeAsync(PermanentCode);
            if (Student != null)
                if (Student.Password == password) return Student;
            return null;
        }
    }
}