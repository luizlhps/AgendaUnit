using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;

namespace AgendaUnit.Domain.Services;

public class UserService : BaseService<User>, IUserService
{
    public readonly IUserRepository _userRepository;

    public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    async public Task<User> GetByIdWithCompany(int id)
    {
        return await _userRepository.GetByIdWithCompany(id);

    }

}
