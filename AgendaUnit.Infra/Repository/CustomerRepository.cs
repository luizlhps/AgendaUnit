using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.models;
using AgendaUnit.Infra.Context;
using AgendaUnit.Infrastructure.Repositories;

namespace AgendaUnit.Infra.Repository;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

}
