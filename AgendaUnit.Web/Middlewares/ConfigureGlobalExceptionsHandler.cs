using AgendaUnit.Shared.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AgendaUnit.Web.Middlewares;

public static class ApiExceptionsMiddlewaresExtensions
{
    public static void ConfigureGlobalExceptionsHandler(this IApplicationBuilder app, IHostEnvironment environment)
    {

        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature != null)
                {
                    Exception exception = contextFeature.Error;

                    object response;

                    response = new
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Ocorreu um erro interno no servidor.",
                        Name = exception.GetType().Name,
                        StackTrace = environment.IsDevelopment() ? exception.StackTrace : null
                    };

                    if (exception is BaseException baseException)
                    {
                        context.Response.StatusCode = baseException.StatusCode;

                        response = new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = baseException.Message,
                            Name = exception.GetType().Name,
                            StackTrace = environment.IsDevelopment() ? exception.StackTrace : null
                        };

                        if (exception is BadRequestException badRequestException)
                        {
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;

                            response = new
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = badRequestException.Message,
                                Name = exception.GetType().Name,
                                ErrorsFields = badRequestException.ModelState
                                    .Where(fields => fields.Value.Errors.Count > 0)
                                    .ToDictionary(
                                        kvp => kvp.Key,
                                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                                    ),
                                StackTrace = environment.IsDevelopment() ? exception.StackTrace : null,
                            };
                        }
                    }
                    else if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                    {
                        response = new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Acesso não autorizado.",
                            Name = exception?.GetType().Name,
                            StackTrace = environment.IsDevelopment() ? exception?.StackTrace : null
                        };
                    }
                    else if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                    {
                        response = new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Acesso não permitido.",
                            Name = exception.GetType().Name,
                            StackTrace = environment.IsDevelopment() ? exception.StackTrace : null
                        };
                    }

                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    };

                    var jsonResponse = JsonSerializer.Serialize(response, options);
                    await context.Response.WriteAsync(jsonResponse);
                }
            });
        });
    }
}
