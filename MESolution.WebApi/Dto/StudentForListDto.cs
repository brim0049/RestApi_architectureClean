namespace MESolution.WebApi.Dto
{
    public class StudentForListDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string PermanentCode { get; set; }
        public string Password { get; set; }
        public string CorrespondenceAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Citizenship { get; set; }
        public string ImmigrationCode { get; set; }
        public string DatePermanentResident { get; set; }
        public string LanguagePreference { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public string DateMaritalStatus { get; set; }
        public string InstitutionName { get; set; }
        public string ProgramCode { get; set; }
        public int CreditCount { get; set; }
    }
}
