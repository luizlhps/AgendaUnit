using AgendaUnit.Application.DTO;
using AgendaUnit.Application.DTO.CompanyDto;
using AgendaUnit.Application.DTO.ServiceDto;
using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        /*  CreateMap<User, UserDto>().ReverseMap(); */
        CreateMap<Service, ServiceDto>().ReverseMap();
        /*  CreateMap<Customer, CustomerDto>().ReverseMap(); */
        CreateMap<Company, CompanyDto>().ReverseMap();
    }
}