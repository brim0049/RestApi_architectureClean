namespace MESolution.WebApi.Dto
{
    public class RequestForListDto
    {

        public decimal EmploymentIncomeLastYear { get; set; }
        public decimal OtherIncomeLastYear { get; set; }
        public decimal PotentialIncomeCurrentYear { get; set; }
        public decimal LoanPortion { get; set; } // Portion de prêt calculée
        public decimal GrantPortion { get; set; } // Portion de bourse calculée
    }
}
