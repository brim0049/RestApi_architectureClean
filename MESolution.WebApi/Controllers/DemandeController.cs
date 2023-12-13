using AutoMapper;
using Azure.Core;
using MESolution.Core.Entities;
using MESolution.Core.Interfaces;
using MESolution.WebApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MESolution.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MESolution.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DemandeController : ControllerBase
    {
        private readonly IDemandeService _requestService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        public DemandeController(IDemandeService requestService, IStudentService studentService, IMapper mapper)
        {
            _requestService = requestService;
            _studentService = studentService;
            _mapper = mapper;
        }

        [HttpGet("Student/{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var Student = await _studentService.GetStudentById(id);
            if (Student == null)
            {
                return BadRequest("Student not found");
            }


            var StudentToReturn = _mapper.Map<StudentForListDto>(Student);

            return Ok(StudentToReturn);
        }

        [HttpGet("StudentRequests/{id}")]
        public async Task<IActionResult> GetStudentRequests(int id)
        {
            var requests = await _requestService.GetUserRequests(id);

            var requestsToReturn = _mapper.Map<IEnumerable<RequestForListDto>>(requests);

            return Ok(requestsToReturn);
        }
        [HttpGet("StudentRequestItems/{StudentId}")]
        public async Task<IActionResult> GetUserRequestItems(int studentId)
        {
            var student = await _studentService.GetStudentById(studentId);

            if (student == null)
            {
                return BadRequest("Student not found");
            }

            var studentRequests = await _requestService.GetUserRequests(studentId);

            if (studentRequests == null || studentRequests.Count == 0)
            {
                return NotFound("No requests found for this student");
            }

            var allUserRequestItems = new List<TransactionForListDto>();

            foreach (var request in studentRequests)
            {
                var requestItems = await _requestService.GetRequestItems(request.Id);

                if (requestItems != null)
                {
                    var requestItemsDto = _mapper.Map<List<TransactionForListDto>>(requestItems);
                    allUserRequestItems.AddRange(requestItemsDto);
                }
            }

            return Ok(allUserRequestItems);
        }


        [HttpPost("NewStudentRequest/{StudentId}")]
        public async Task<IActionResult> AddRequest(int studentId, RequestForListDto requestForListDto)
        {
            var student = await _studentService.GetStudentById(studentId);
            if (student == null)
            {
                return BadRequest("Student not found");
            }
            // Extraire les informations pertinentes de requestForListDto
            decimal currentYearIncome = requestForListDto.EmploymentIncomeLastYear;
            decimal lastYearOtherIncome = requestForListDto.OtherIncomeLastYear;
            decimal employmentIncome = requestForListDto.PotentialIncomeCurrentYear;
            FinancialTransaction ft = new FinancialTransaction();
            // Calculer le montant de l'aide en utilisant CalculateAidAmount
            decimal aidAmount =ft.CalculateAidAmount(currentYearIncome, lastYearOtherIncome, employmentIncome);

            // Déterminer le type d'aide en utilisant DetermineAidType
            FinancialTransactionType aidType = ft.DetermineAidType(aidAmount);
            var request = new FinancialAidApplication(student);

            // Mapper les données du DTO de la requête vers l'objet de la demande
            _mapper.Map(requestForListDto, request);

            request = await _requestService.AddRequest(request);
            // Créer un objet TransactionForCreateDto avec les détails de la demande
            
            var requestItemForCreateDto = new FinancialTransaction
            {
                // Ajouter les détails pertinents pour la transaction, comme le montant et le type d'aide
                // Ces informations peuvent dépendre de votre modèle de données pour la transaction
                TransactionDate = DateTime.Now,
                TransactionType = aidType,
                Amount = aidAmount
            };

            // Ajoutez cette transaction à la demande d'aide financière
            var addedRequestItem = await _requestService.AddRequestItem(request.Id, requestItemForCreateDto);

            var requestToReturn = _mapper.Map<FinancialAidApplication>(request);

            return Ok(requestToReturn);
        }
        

        [HttpPost("NewRequestItem/{requestId}")]
        public async Task<IActionResult> AddRequestItem(int requestId, TransactionForCreateDto requestItemForCreateDto)
        {
            var request = await _requestService.GetRequest(requestId);
            if (request == null) return BadRequest("Request not found");
            var requestItemToCreate = _mapper.Map<FinancialTransaction>(requestItemForCreateDto);
            var requestUpdated = await _requestService.AddRequestItem(requestId, requestItemToCreate);
            var requestToReturn = _mapper.Map<RequestForDetailedDto>(requestUpdated);
            return Ok(requestToReturn);
        }


        /*
        [HttpGet("RequestItems/{id}")]
        public async Task<IActionResult> GetRequestItems(int id)
        {
            var requestItems = await _requestService.GetRequestItems(id);

            var requestItemsToReturn = _mapper.Map<IEnumerable<TransactionForListDto>>(requestItems);

            return Ok(requestItemsToReturn);
        }
        

        [HttpPost("NewStudentRequest/{studentId}")]
        public async Task<IActionResult> AddRequest(int studentId, RequestForListDto requestForListDto)
        {
            var student = await _studentService.GetStudentById(studentId);
            if (student == null) return BadRequest("Student not found");
            var request = new FinancialAidApplication(student);
            request = await _requestService.AddRequest(request);
            var requestToReturn = _mapper.Map<FinancialAidApplication>(request);
            return Ok(requestToReturn);
        }
        */





    }
}
