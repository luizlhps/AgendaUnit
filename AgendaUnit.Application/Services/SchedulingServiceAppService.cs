using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;

using AutoMapper;

namespace AgendaUnit.Application.Services;

public class SchedulingServiceAppService : Crud<Domain.Models.SchedulingService>, ISchedulingServiceAppService
{
    public SchedulingServiceAppService(IUnitOfWork unitOfWork, IMapper mapper, IServiceProvider serviceProvider) : base(unitOfWork, mapper, serviceProvider)
    {
    }
}



