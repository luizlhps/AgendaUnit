namespace AgendaUnit.Application.DTO.AuthenticationManagerDto;

public class LoginResponseDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expires { get; set; }
}
