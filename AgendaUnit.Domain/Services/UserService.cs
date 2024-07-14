using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;

namespace AgendaUnit.Domain.Services;

public class UserService : BaseService<User, IUserRepository>, IUserService
{
    public readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository) : base(userRepository)
    {
        _userRepository = userRepository;
    }


    async public Task<User> GetByIdWithCompany(int id)
    {
        return await _userRepository.GetByIdWithCompany(id);

    }

}
