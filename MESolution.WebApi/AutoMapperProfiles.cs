using AutoMapper;
using Azure.Core;
using MESolution.Core.Entities;
using MESolution.WebApi.Dto;

namespace MESolution.WebApi
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<StudentForRegisterDto, Student>();
            CreateMap<Student, StudentForDetailedDto>();
            CreateMap<Student, StudentForListDto>();
            CreateMap<RequestForListDto, Request>();
            CreateMap<FinancialAidApplication, RequestForListDto>();
            CreateMap<StudentForListDto, Student>();
            CreateMap<RequestForListDto, FinancialAidApplication>();
            CreateMap<TransactionForCreateDto, FinancialTransaction>();
            CreateMap<FinancialTransaction, TransactionForListDto>();
            CreateMap<FinancialAidApplication, RequestForDetailedDto>();
        }
    }
}
