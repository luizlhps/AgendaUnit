using AgendaUnit.Shared.Attributes;

namespace AgendaUnit.Application.DTO.AuthenticationManagerDto;

[SkipDefaultCommitAttribute]
public class LoginRequestDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}
