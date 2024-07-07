using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.models;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AutoMapper;

namespace AgendaUnit.Application.Services;

public class SchedulingAppService : Crud<Scheduling, ISchedulingRepository, ISchedulingService>, ISchedulingAppService
{
    public SchedulingAppService(ISchedulingRepository repository, IMapper mapper, ISchedulingService baseService) : base(repository, mapper, baseService)
    {
    }
}
