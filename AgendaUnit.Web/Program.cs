
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using AgendaUnit.Application.DTO.AuthenticationManagerDto;
using AgendaUnit.Domain.Models;
using AgendaUnit.Infra;
using AgendaUnit.Shared.Exceptions;
using AgendaUnit.Web.Middlewares;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

/* Infra and AutoMapper */
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

/* HttpContext */
builder.Services.AddHttpContextAccessor();


/* CORS */
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                      });
});

/* JWT */
var secret = builder.Configuration["jwt:Secret"] ?? throw new BaseException("Secret is required");

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(opt =>
{
    opt.SaveToken = true;
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,

        ValidIssuer = builder.Configuration["jwt:Issuer"],
        ValidAudience = builder.Configuration["jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    };

});

/* Memory Cache */
builder.Services.AddMemoryCache();

/* JSON Options */
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

});

//Validators
builder.Services.AddValidatorsFromAssembly(Assembly.Load("AgendaUnit.Application"));

/* Controllers */
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

/* Swagger */
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.ToString());

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AgendaUnit", Version = "v1" });
    c.CustomSchemaIds(s => s.FullName.Replace("+", "."));

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor, insira 'Bearer' [espaço] e o token JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
app.ConfigureGlobalExceptionsHandler(app.Environment);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<TokenMiddleware>();
app.UseMiddleware<SystemConfigurationManagerMiddleware>();

app.MapControllers();

app.Run();
