using AgendaUnit.Domain.models;

namespace AgendaUnit.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByIdWithCompany(int id);
}
