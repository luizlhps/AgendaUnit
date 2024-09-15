using AgendaUnit.Application.DTO;
using AgendaUnit.Application.DTO.CompanyDto;
using AgendaUnit.Domain.Models;
using AutoMapper;
using static AgendaUnit.Application.DTO.CompanyDto.CompanyListedDto;

namespace AgendaUnit.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        /*  CreateMap<User, UserDto>().ReverseMap(); */
        CreateMap<Service, ServiceDto>().ReverseMap();
        CreateMap<Scheduling, SchedulingDto>().ReverseMap();
        /*  CreateMap<Customer, CustomerDto>().ReverseMap(); */
        CreateMap<Company, CompanyDto>().ReverseMap();
    }
}