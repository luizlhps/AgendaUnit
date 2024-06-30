using AgendaUnit.Application.DTO;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.models;
using AgendaUnit.Domain.Services;
using AutoMapper;

namespace AgendaUnit.Application.Services;

public class ServiceAppService : Crud<Service, IServiceRepository, IServiceService>, IServiceAppService
{
    public ServiceAppService(IServiceRepository repository, IMapper mapper, IServiceService baseService) : base(repository, mapper, baseService)
    {
    }

}
