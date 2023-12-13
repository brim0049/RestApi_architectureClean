using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using MESolution.Core;
using MESolution.Core.Interfaces;
using MESolution.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MESolution.WebApi.Dto;
using MESolution.Core.Entities;

namespace MESolution.WebApi.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class AuthorizationController : ControllerBase
        {
            private readonly IStudentService _StudentService;
            private readonly IMapper _mapper;
            private readonly IConfiguration _config;
            public AuthorizationController(IStudentService StudentService, IConfiguration config, IMapper mapper)
            {
                _StudentService = StudentService;
                _config = config;
                _mapper = mapper;
            }


            [HttpPost("registration")]
            public async Task<IActionResult> Register(StudentForRegisterDto StudentForRegisterDto)
            {
                StudentForRegisterDto.SocialSecurityNumber = StudentForRegisterDto.SocialSecurityNumber.ToLower();
                if (await _StudentService.GetStudentByEmail(StudentForRegisterDto.SocialSecurityNumber) != null)
                    return BadRequest("Student already exists");

                var StudentToCreate = _mapper.Map<Student>(StudentForRegisterDto);

                var createdStudent = await _StudentService.RegisterStudent(StudentToCreate);
             
                var StudentToReturn = _mapper.Map<StudentForDetailedDto>(createdStudent);
                return Ok(StudentToReturn);
            }

        [HttpPost("login")]
        public async Task<IActionResult> Login(StudentForLoginDto StudentForLoginDto)
        {
            var Student = await _StudentService.AuthenticateStudent(StudentForLoginDto.PermanentCode
                , StudentForLoginDto.Password);

            if (Student == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Student.Id.ToString()),
               new Claim(ClaimTypes.Name, Student.PermanentCode)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var StudentToReturn = _mapper.Map<StudentForListDto>(Student);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                StudentToReturn
            });
        }

    }
    }

