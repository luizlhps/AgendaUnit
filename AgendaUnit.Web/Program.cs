
using System.Text.Json.Serialization;
using AgendaUnit.Infra;
using AgendaUnit.Web.Middlewares;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.ToString());
});

var app = builder.Build();
app.ConfigureGlobalExceptionsHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
