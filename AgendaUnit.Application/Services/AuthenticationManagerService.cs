
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AgendaUnit.Application.DTO.AuthenticationManagerDto;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Shared.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace AgendaUnit.Application.Services;
public class AuthenticationManagerService : IAuthenticationManagerService
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AuthenticationManagerService(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {

        var userListDto = new UserListDto
        {
            Email = loginRequestDto.Email,
        };

        var pageResultUser = await _userService.GetAll<UserListDto, UserListedDto>(userListDto);

        if (pageResultUser.Items.Count == 0)
        {
            throw new NotFoundException("Email ou senha inválidos.");
        }

        var user = pageResultUser.Items.First();

        var loginValid = BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, user.Password);

        if (!loginValid)
        {
            throw new NotFoundException("Email ou senha inválidos.");
        }

        var token = GenerateJwtToken(loginRequestDto.Email, "1");

        var loginResponseDto = new LoginResponseDto
        {
            Token = token,
            /*   ExpiresAt = token.ValidTo, */
        };

        return loginResponseDto;
    }

    private string GenerateJwtToken(string email, string role)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, role),
        };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["jwt:Issuer"],
            audience: _configuration["jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);



        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
