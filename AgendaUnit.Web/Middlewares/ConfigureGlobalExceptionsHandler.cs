using AgendaUnit.Shared.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace AgendaUnit.Web.Middlewares;

public static class ApiExceptionsMiddlewaresExtensions
{
    public static void ConfigureGlobalExceptionsHandler(this IApplicationBuilder app)
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

                    var response = new

                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Ocorreu um erro interno no servidor.",
                        Name = exception.GetType().Name,
                    };

                    if (exception is BaseException baseException)
                    {
                        context.Response.StatusCode = baseException.StatusCode;

                        response = new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = baseException.Message,
                            Name = exception.GetType().Name,
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
