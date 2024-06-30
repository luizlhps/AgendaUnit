using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.models;
using AgendaUnit.Infra.Context;
using AgendaUnit.Infrastructure.Repositories;

namespace AgendaUnit.Infra.Repository;

public class ServiceRepository : BaseRepository<Service>, IServiceRepository
{
    public ServiceRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

}
