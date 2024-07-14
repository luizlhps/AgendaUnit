using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Models;

namespace AgendaUnit.Domain.Services;

public class SchedulingService : BaseService<Scheduling, ISchedulingRepository>, ISchedulingService
{
    public readonly ISchedulingRepository _schedulingRepository;
    public SchedulingService(ISchedulingRepository schedulingRepository) : base(schedulingRepository)
    {
        _schedulingRepository = schedulingRepository;
    }

}
