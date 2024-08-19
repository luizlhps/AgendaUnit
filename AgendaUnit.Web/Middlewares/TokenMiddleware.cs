using System.IdentityModel.Tokens.Jwt;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Shared.CrossCutting;
using AgendaUnit.Shared.Exceptions;
using Microsoft.Extensions.DependencyInjection;

public class TokenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public TokenMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        if (authHeader != null && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken != null)
            {
                var username = jwtToken.Claims.FirstOrDefault(c => c.Type == "username")?.Value;

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var userAppService = scope.ServiceProvider.GetRequiredService<IUserAppService>();
                    var common = scope.ServiceProvider.GetRequiredService<ICommon>();

                    var userListDto = new UserListDto { Username = username };
                    var pageResultUser = await userAppService.GetAll<UserListDto, UserListedDto>(userListDto);

                    if (pageResultUser.Items.Count > 0)
                    {
                        var user = pageResultUser.Items.First();

                        common.UserRole = pageResultUser.Items.First().Role.Name;
                        common.UserId = pageResultUser.Items.First().Id;
                    }
                    else
                    {
                        throw new UnauthorizedException("Token inválido");
                    }
                }
            }
        }

        await _next(context);
    }
}
