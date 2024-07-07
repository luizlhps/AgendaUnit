using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.models;
using AgendaUnit.Domain.Models;
using AgendaUnit.Infra.Context;
using AgendaUnit.Infrastructure.Repositories;

namespace AgendaUnit.Infra.Repository;
public class SchedulingRepository : BaseRepository<Scheduling>, ISchedulingRepository
{
    public SchedulingRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
