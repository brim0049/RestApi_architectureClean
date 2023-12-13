using System.ComponentModel.DataAnnotations;

namespace MESolution.WebApi.Dto
{
    public class StudentForRegisterDto
    {
       

        public string SocialSecurityNumber { get; set; }
        public string DateOfBirth { get; set; }

    }
}
