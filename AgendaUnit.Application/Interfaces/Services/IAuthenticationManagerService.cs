using AgendaUnit.Application.DTO.AuthenticationManagerDto;
using AgendaUnit.Application.DTO.UserDto;

namespace AgendaUnit.Application.Interfaces.Services;
public interface IAuthenticationManagerService
{
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<LoginResponseDto> RefreshToken(UserToken userToken);
}

