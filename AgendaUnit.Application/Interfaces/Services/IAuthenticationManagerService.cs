using AgendaUnit.Application.DTO.AuthenticationManagerDto;

namespace AgendaUnit.Application.Interfaces.Services;
public interface IAuthenticationManagerService
{
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

}

