using AgendaUnit.Application.DTO;
using AgendaUnit.Domain.models;
using AutoMapper;

namespace AgendaUnit.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<BusinessHours, BusinessHoursDto>().ReverseMap();
        CreateMap<Service, ServiceDto>().ReverseMap();
        CreateMap<Scheduling, SchedulingDto>().ReverseMap();
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<Company, CompanyDto>().ReverseMap();
    }
}