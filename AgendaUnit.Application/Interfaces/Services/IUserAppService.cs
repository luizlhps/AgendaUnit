using AgendaUnit.Application.DTO;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Services;
using AgendaUnit.Domain.Models;

namespace AgendaUnit.Application.Interfaces.Services;

public interface IUserAppService : ICrudAppService<User>
{

    Task<UserCreatedDto> Register(UserCreateDto userCreateDto);
    Task<UserObtainedDto> GetInfo();
}
