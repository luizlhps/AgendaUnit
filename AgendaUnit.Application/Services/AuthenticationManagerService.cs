
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AgendaUnit.Application.DTO.AuthenticationManagerDto;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Interfaces;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Dtos;
using AgendaUnit.Shared.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace AgendaUnit.Application.Services;
public class AuthenticationManagerService : IAuthenticationManagerService
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    private readonly IUserMemoryCacheService _userMemoryCacheService;

    public AuthenticationManagerService(IUserService userService, IConfiguration configuration, IUserMemoryCacheService userMemoryCacheService)
    {
        _userMemoryCacheService = userMemoryCacheService;
        _userService = userService;
        _configuration = configuration;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {

        var userListDto = new UserListDto
        {
            Username = loginRequestDto.Username,
        };

        var pageResultUser = await _userService.GetAll<UserListDto, UserListedDto>(userListDto);

        if (pageResultUser.Items.Count == 0)
        {
            throw new UnauthorizedException("Login ou senha inválidos.");
        }

        var user = pageResultUser.Items.First();

        var loginValid = BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, user.Password);

        if (!loginValid)
        {
            throw new UnauthorizedException("Login ou senha inválidos.");
        }

        var token = GenerateJwtToken(loginRequestDto.Username, user.Role.Name);

        var loginResponseDto = new LoginResponseDto
        {
            Token = token,
        };

        /*         var userCacheDto = new UserCacheDto
                {
                    RoleId = user.RoleId,
                    Token = token,
                    userId = user.Id,
                    email = user.Email
                }; */
        /* 
                _userMemoryCacheService.SetData(userCacheDto); */

        return loginResponseDto;
    }

    private string GenerateJwtToken(string login, string role)
    {
        var claims = new[]
        {
            new Claim("login", login),
            new Claim("role", role),
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
