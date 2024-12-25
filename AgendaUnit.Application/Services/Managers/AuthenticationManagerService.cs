
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AgendaUnit.Application.DTO.AuthenticationManagerDto;
using AgendaUnit.Application.DTO.SystemConfigurationManager;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Interfaces;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.CrossCutting;
using AgendaUnit.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace AgendaUnit.Application.Services.Managers;
public class AuthenticationManagerService : IAuthenticationManagerService
{
    private readonly IUserAppService _userAppService;
    private readonly IConfiguration _configuration;
    private readonly ICommon _common;
    private readonly IUserMemoryCacheService _userMemoryCacheService;
    private readonly ISystemConfigurationManagerService _systemConfigurationManagerService;

    public AuthenticationManagerService(
        ICommon common,
        ISystemConfigurationManagerService systemConfigurationManagerService,
        IUserAppService userAppService,
        IConfiguration configuration,
        IUserMemoryCacheService userMemoryCacheService
        )
    {
        _userMemoryCacheService = userMemoryCacheService;
        _configuration = configuration;
        _userAppService = userAppService;
        _common = common;
        _systemConfigurationManagerService = systemConfigurationManagerService;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var userListDto = new UserListDto
        {
            Username = loginRequestDto.Username,
        };

        var pageResultUser = await _userAppService.GetAll<UserListDto, UserListedDto>(userListDto);

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
        var refreshToken = GenerateRefreshToken();

        #region update refresh token for user

        var userUpdateDto = new UserRefreshTokenUpdateDto
        {
            Id = user.Id,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTimeOffset.UtcNow.AddDays(7),
        };

        await _userAppService.Update<UserRefreshTokenUpdateDto, UserUpdatedDto>(userUpdateDto);

        #endregion

        var loginResponseDto = new LoginResponseDto
        {
            Token = token,
            Expires = DateTimeOffset.UtcNow.AddHours(1),
            RefreshToken = refreshToken,
        };

        return loginResponseDto;
    }

    public async Task<LoginResponseDto> RefreshToken(UserToken userToken)
    {

        var userObtained = await _userAppService.GetById<UserObtainedDto>(_common.UserId);

        if (userObtained == null)
        {
            throw new UnauthorizedException("Usuário não encontrado.");
        }

        if (userToken.RefreshToken != userObtained.RefreshToken | userObtained.RefreshTokenExpiryTime < DateTimeOffset.UtcNow)
        {
            throw new UnauthorizedException("RefreshToken inválido.");
        }

        var refreshToken = GenerateRefreshToken();
        var token = GenerateJwtToken(userObtained.Username, _common.UserRole);

        var userUpdateDto = new UserRefreshTokenUpdateDto
        {
            Id = userObtained.Id,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTimeOffset.UtcNow.AddDays(7),
        };

        var userUpdatedDto = await _userAppService.Update<UserRefreshTokenUpdateDto, UserUpdatedDto>(userUpdateDto);

        return new LoginResponseDto
        {
            Token = token,
            Expires = DateTimeOffset.UtcNow.AddHours(1),
            RefreshToken = refreshToken,
        };

    }

    private string GenerateJwtToken(string username, string role)
    {
        var claims = new[]
        {
            new Claim("username", username),
            new Claim("role", role),
        };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["jwt:Issuer"],
            audience: _configuration["jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}

