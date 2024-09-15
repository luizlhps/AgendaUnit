using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AutoMapper;

namespace AgendaUnit.Application.Services;

public class SchedulingAppService : Crud<Scheduling, ISchedulingService>, ISchedulingAppService
{
    public SchedulingAppService(IUnitOfWork unitOfWork, IMapper mapper, ISchedulingService baseService, IServiceProvider serviceProvider) : base(unitOfWork, mapper, baseService, serviceProvider)
    {
    }
}
