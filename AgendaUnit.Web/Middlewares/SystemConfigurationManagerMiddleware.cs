using System.IdentityModel.Tokens.Jwt;
using System.Net;
using AgendaUnit.Application.DTO.SystemConfigurationManager;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Shared.Attributes;
using AgendaUnit.Shared.CrossCutting;
using AgendaUnit.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

public class SystemConfigurationManagerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public SystemConfigurationManagerMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var userId = context.Items["UserId"];

        if (endpoint != null)
        {
            var skipVerifySystemConfigAttribute = endpoint.Metadata.GetMetadata<SkipVerifySystemConfigAttribute>();

            if (skipVerifySystemConfigAttribute == null)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {

                    if (userId == null)
                    {
                        var modelState = new ModelStateDictionary();
                        modelState.AddModelError("UserId", "Não foi possível encontrar o usuário no contexto");

                        throw new BadRequestException(modelState, "Não foi possível encontrar o usuário no contexto");
                    }

                    var systemConfigurationManagerService = scope.ServiceProvider.GetRequiredService<ISystemConfigurationManagerService>();

                    var systemConfiguration = await systemConfigurationManagerService.VerifyAccountConfiguration();

                    if (!systemConfiguration.SystemConfigurated)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            error = "Sistema não está configurado na etapa requerida.",
                            name = "SYSTEM_CONFIG",
                            etapa = systemConfiguration.Step.ToString()
                        });

                        return;
                    }

                }
            }

        }


        await _next(context);

    }
}
