using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Models;

namespace AgendaUnit.Domain.Interfaces.Services;

public interface IUserService : IBaseService<User, IUserRepository>
{
    Task<User> GetByIdWithCompany(int id);
}
