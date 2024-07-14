using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Models;
using AgendaUnit.Infra.Context;
using AgendaUnit.Infrastructure.Repositories;

namespace AgendaUnit.Infra.Repository;

public class BusinessHoursRepository : BaseRepository<BusinessHours>, IBusinessHoursRepository
{
    public BusinessHoursRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

}
