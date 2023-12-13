using MESolution.SharedKernel;
using MESolution.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESolution.Core.Entities
{
    public class FinancialAidApplication: BaseEntity, IAggregateRoot
    {
        public FinancialAidApplication()
        { 
        
        }
        public FinancialAidApplication(Student student)
        { 
            Student = student;
        }
        public Student Student { get; set; }
        public int StudentId { get; set; }
      
        public decimal EmploymentIncomeLastYear { get; set; }
        public decimal OtherIncomeLastYear { get; set; }
        public decimal PotentialIncomeCurrentYear { get; set; }
        public decimal LoanPortion { get; set; } // Portion de prêt calculée
        public decimal GrantPortion { get; set; } // Portion de bourse calculée
        public virtual List<FinancialTransaction> FinancialTransactions { get; private set; } = new List<FinancialTransaction>();
        public void AddRequestItem(FinancialTransaction requestItem)
        {
            FinancialTransactions.Add(requestItem);
        }

        public void RemoveRequestItem(FinancialTransaction requestItem)
        {
            FinancialTransactions.Remove(requestItem);
        }
        // Méthode pour calculer le total des transactions de prêts
        public decimal CalculateTotalLoanTransactions()
        {
            return FinancialTransactions
                .Where(t => t.TransactionType == FinancialTransactionType.Loan)
                .Sum(t => t.Amount);
        }

        // Méthode pour calculer le total des transactions de bourse
        public decimal CalculateTotalGrantTransactions()
        {
            return FinancialTransactions
                .Where(t => t.TransactionType == FinancialTransactionType.Grant)
                .Sum(t => t.Amount);
        }

        // Méthode pour mettre à jour les portions de prêt et de bourse
        public void UpdateLoanAndGrantPortions()
        {
            LoanPortion = CalculateTotalLoanTransactions();
            GrantPortion = CalculateTotalGrantTransactions();
        }
        


        public Dictionary<int, FinancialTransaction> GetRequestItemsDictionary()
        {

            var requestItemsDictionary = new Dictionary<int, FinancialTransaction>();
            foreach (var requestItem in FinancialTransactions)
            {
                requestItemsDictionary.Add(requestItem.Id, requestItem);
            }

            return requestItemsDictionary;
        }
    }
}
