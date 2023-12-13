using MESolution.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESolution.Core.Interfaces
{
    public interface IDemandeService
    {
        Task<FinancialAidApplication> GetRequest(int id);
        Task<FinancialAidApplication> AddRequest(FinancialAidApplication request);
        Task UpdateRequest(FinancialAidApplication request);
        Task DeleteRequest(FinancialAidApplication request);
       Task<FinancialAidApplication> AddRequestItem(int requestId, FinancialTransaction requestItem);
        Task UpdateRequestItem(int requestId, FinancialTransaction requestItem);
        Task<FinancialAidApplication> DeleteRequestItem(int requestId, int requestItemId);
        Task<FinancialAidApplication> SubmitRequest(FinancialAidApplication request, string directory);
        Task<IReadOnlyList<FinancialAidApplication>> GetUserRequests(int id);
        Task<IReadOnlyList<FinancialTransaction>> GetRequestItems(int id);
    }
}
