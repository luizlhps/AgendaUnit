using AgendaUnit.Application.Interfaces;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Application.Mappers;
using AgendaUnit.Application.Services;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AgendaUnit.Infra.Context;
using AgendaUnit.Infra.CrossCutting.MemoryCacheServices;
using AgendaUnit.Infra.Repository;
using AgendaUnit.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaUnit.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddServices()
            .AddPersistence(configuration);

        return services;
    }


    private static IServiceCollection AddServices(this IServiceCollection services)
    {

        // application services
        services.AddScoped<IBusinessHoursAppService, BusinessHoursAppService>();
        services.AddScoped<ICompanyAppService, CompanyAppService>();
        services.AddScoped<ICustomerAppService, CustomerAppService>();
        services.AddScoped<ISchedulingAppService, SchedulingAppService>();
        services.AddScoped<IServiceAppService, ServiceAppService>();
        services.AddScoped<IUserAppService, UserAppService>();
        services.AddScoped<IAuthenticationManagerService, AuthenticationManagerService>();

        // domain services
        services.AddScoped<IBusinessHoursService, BusinessHoursService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ISchedulingService, SchedulingService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IServiceService, ServiceService>();


        // repositories
        services.AddScoped<IBusinessHoursRepository, BusinessHoursRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ISchedulingRepository, SchedulingRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();

        // AutoMapper 
        services.AddAutoMapper(typeof(MappingProfile));

        //MemoryCacheService
        services.AddSingleton<IUserMemoryCacheService, UserMemoryCacheService>();


        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration["ConnectionStrings:PostSqlDBConnection"];

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }


}