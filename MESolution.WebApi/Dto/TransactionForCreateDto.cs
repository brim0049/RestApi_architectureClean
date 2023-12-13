using MESolution.Core.Entities;

namespace MESolution.WebApi.Dto
{
    public class TransactionForCreateDto
    {
        public DateTime TransactionDate { get; set; }
        public FinancialTransactionType TransactionType { get; set; } // Type de transaction (prêt ou bourse)
        public decimal Amount { get; set; }
    }
}
