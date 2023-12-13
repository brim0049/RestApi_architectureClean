using MESolution.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESolution.Core.Entities
{
    public class FinancialTransaction: BaseEntity
    {
        public int FinancialTransactionId { get; set; }
        public int FinancialAidApplicationId { get; set; } // Clé étrangère vers la demande d'aide financière
        public DateTime TransactionDate { get; set; }
        public FinancialTransactionType TransactionType { get; set; } // Type de transaction (prêt ou bourse)
        public decimal Amount { get; set; }
        public FinancialTransaction()
        {
            
        }
        public FinancialTransaction(int transactionId, int aidApplicationId, DateTime date, FinancialTransactionType type, decimal amount)
        {
            FinancialTransactionId = transactionId;
            FinancialAidApplicationId = aidApplicationId;
            TransactionDate = date;
            TransactionType = type;
            Amount = amount;
        }
        public decimal CalculateAidAmount(decimal currentYearIncome, decimal lastYearOtherIncome, decimal employmentIncome)
        {
            // Appliquer des pondérations à chaque critère de revenu
            decimal currentYearWeight = 0.6m;
            decimal lastYearOtherWeight = 0.3m;
            decimal employmentWeight = 0.1m;

            // Calculer le montant de l'aide en utilisant les pondérations et les revenus
            decimal aidAmount = (currentYearIncome * currentYearWeight) +
                                (lastYearOtherIncome * lastYearOtherWeight) +
                                (employmentIncome * employmentWeight);

            return aidAmount;
        }
        public FinancialTransactionType DetermineAidType(decimal aidAmount)
        {
            // Ceci est un exemple simple. Vous pouvez utiliser des seuils spécifiques pour décider du type d'aide.
            return aidAmount > 1000 ? FinancialTransactionType.Loan : FinancialTransactionType.Grant;
        }
    }

    public enum FinancialTransactionType
    {
        Loan,
        Grant
    }
}
