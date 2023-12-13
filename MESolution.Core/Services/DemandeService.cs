using MESolution.Core.Entities;
using MESolution.Core.Interfaces;
using MESolution.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESolution.Core.Services
{
    public class DemandeService:IDemandeService
    {
        private readonly IDemandeRepository _RequestRepository;
        private readonly IStudentRepository _UserRepository;
        private readonly IBackEndSystemService _IBackEndSystemService;
        public DemandeService(IDemandeRepository requestRepository, IStudentRepository userRepository, IBackEndSystemService backEndSystemService)
        {
            _RequestRepository = requestRepository;
            _UserRepository = userRepository;
            _IBackEndSystemService = backEndSystemService;
        }


        public async Task<FinancialAidApplication> GetRequest(int id)
        {
            return await _RequestRepository.GetByIdWithRequestItemsAsync(id);
        }
        public async Task<FinancialAidApplication> AddRequest(FinancialAidApplication request)
        {
            return await _RequestRepository.AddAsync(request);
        }
        public async Task UpdateRequest(FinancialAidApplication request)
        {
            await _RequestRepository.UpdateAsync(request);
        }
        public async Task DeleteRequest(FinancialAidApplication request)
        {
            await _RequestRepository.DeleteAsync(request);
        }
        public async Task<FinancialAidApplication> AddRequestItem(int requestId, FinancialTransaction requestItem)
        {
            FinancialAidApplication request = await _RequestRepository.GetByIdAsync(requestId);
            if (request != null)
            {
                request.AddRequestItem(requestItem);
                request.UpdateLoanAndGrantPortions(); // Mettre à jour les portions de prêt et de bourse


            }
            await _RequestRepository.UpdateAsync(request);
            return await _RequestRepository.GetByIdWithRequestItemsAsync(request.Id);
        }
        public async Task UpdateRequestItem(int requestId, FinancialTransaction requestItem)
        {
            FinancialAidApplication request = await _RequestRepository.GetByIdWithRequestItemsAsync(requestId);
            FinancialTransaction ri = request.GetRequestItemsDictionary()[requestItem.Id];
            if (ri != null)
            {
                request.RemoveRequestItem(ri);
            }
            request.AddRequestItem(requestItem);
            await _RequestRepository.UpdateAsync(request);
        }
        public async Task<FinancialAidApplication> DeleteRequestItem(int requestId, int requestItemId)
        {
            FinancialAidApplication request = await _RequestRepository.GetByIdWithRequestItemsAsync(requestId);
            FinancialTransaction requestItem = request.GetRequestItemsDictionary()[requestItemId];
            request.RemoveRequestItem(requestItem);
            await _RequestRepository.UpdateAsync(request);
            return request;
        }
        public async Task<FinancialAidApplication> SubmitRequest(FinancialAidApplication request, string directory)
        {
            await _IBackEndSystemService.sendRequestToBackEnd(request, directory);
         //   request.IsSubmitted = true;
            await UpdateRequest(request);
            return await _RequestRepository.GetByIdWithRequestItemsAsync(request.Id);
        }
        public async Task<IReadOnlyList<FinancialAidApplication>> GetUserRequests(int id)
        {
            RequestByUser spec = new RequestByUser(id);
            IReadOnlyList<FinancialAidApplication> requests = await _RequestRepository.ListAsync(spec);
            List<FinancialAidApplication> requestsToReturn = new List<FinancialAidApplication>();
            foreach (FinancialAidApplication request in requests)
                requestsToReturn.Add(await _RequestRepository.GetByIdWithRequestItemsAsync(request.Id));
            return (IReadOnlyList<FinancialAidApplication>)requestsToReturn;
        }

        public async Task<IReadOnlyList<FinancialTransaction>> GetRequestItems(int id)
        {
            FinancialAidApplication request = await _RequestRepository.GetByIdWithRequestItemsAsync(id);
            return (IReadOnlyList<FinancialTransaction>)request.FinancialTransactions;
        }

     
    }
}
