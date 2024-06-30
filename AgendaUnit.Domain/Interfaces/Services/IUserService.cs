using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.models;

namespace AgendaUnit.Domain.Interfaces.Services;

public interface IUserService : IBaseService<User, IUserRepository>
{
    Task<User> GetByIdWithCompany(int id);
}
