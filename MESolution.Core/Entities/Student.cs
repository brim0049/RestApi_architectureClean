using MESolution.SharedKernel;
using MESolution.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESolution.Core.Entities
{
    public class Student : BaseEntity, IAggregateRoot
    {
        public Student() { }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string? PermanentCode { get; set; }
        public string? Password { get; set; }
        public string? CorrespondenceAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Citizenship { get; set; }
        public string? ImmigrationCode { get; set; }
        public string? DatePermanentResident { get; set; }
        public string? LanguagePreference { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public string? DateMaritalStatus { get; set; }
        public string? InstitutionName { get; set; }
        public string? ProgramCode { get; set; }
        public int? CreditCount { get; set; }
        public Student(
            string firstName,
            string lastName,
       string socialSecurityNumber,
       string dateOfBirth,
       string permanentCode,
       string password,
       string correspondenceAddress,
       string phoneNumber,
       string email,
       string citizenship,
       string immigrationCode,
       string datePermanentResident,
       string languagePreference,
       MaritalStatus maritalStatus,
       string dateMaritalStatus,
       string institutionName, 
       string programCode, 
       int creditCount

       )
        {
            FirstName= firstName;
            LastName= lastName;
            SocialSecurityNumber = socialSecurityNumber;
            DateOfBirth = dateOfBirth;
            PermanentCode = permanentCode;
            Password = password;
            CorrespondenceAddress = correspondenceAddress;
            PhoneNumber = phoneNumber;
            Email = email;
            Citizenship = citizenship;
            ImmigrationCode = immigrationCode;
            DatePermanentResident = datePermanentResident;
            LanguagePreference = languagePreference;
            MaritalStatus = maritalStatus;
            DateMaritalStatus = dateMaritalStatus;
            InstitutionName= institutionName;
            ProgramCode = programCode;
            CreditCount = creditCount;
        }
    }
}
public enum MaritalStatus
{
    Single,
    Married,
}