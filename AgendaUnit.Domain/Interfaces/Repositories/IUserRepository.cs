using AgendaUnit.Domain.Models;

namespace AgendaUnit.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByIdWithCompany(int id);
}
