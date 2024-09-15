using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Models;

namespace AgendaUnit.Domain.Services;

public class SchedulingService : BaseService<Scheduling>, ISchedulingService
{
    public SchedulingService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
