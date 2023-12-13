using AutoMapper;
using MESolution.Core.Entities;
using MESolution.Core.Interfaces;
using MESolution.Core.Services;
using MESolution.WebApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MESolution.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IDemandeService _requestService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        public StudentController(IDemandeService requestService, IStudentService studentService, IMapper mapper)
        {
            _requestService = requestService;
            _studentService = studentService;
            _mapper = mapper;
        }


        [HttpPut("Student/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentForListDto studentForUpdateDto)
        {
            // Récupérer l'étudiant à mettre à jour depuis la base de données
            var studentFromRepo = await _studentService.GetStudentById(id);

            if (studentFromRepo == null)
            {
                return NotFound();
            }

            // Mapper les modifications depuis studentForUpdateDto à studentFromRepo
            _mapper.Map(studentForUpdateDto, studentFromRepo);

            // Effectuer la mise à jour de l'étudiant dans la base de données
            _studentService.UpdateStudent(studentFromRepo);

            // Mapper l'étudiant mis à jour vers un DTO pour la réponse
            var studentToReturn = _mapper.Map<StudentForDetailedDto>(studentFromRepo);

            return Ok(studentToReturn);
        }
       


    }
}
