using AgendaUnit.Application.DTO;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AutoMapper;

namespace AgendaUnit.Application.Services;

public class ServiceAppService : Crud<Service>, IServiceAppService
{
    public ServiceAppService(IUnitOfWork unitOfWork, IMapper mapper, IServiceProvider serviceProvider) : base(unitOfWork, mapper, serviceProvider)
    {
    }
}
