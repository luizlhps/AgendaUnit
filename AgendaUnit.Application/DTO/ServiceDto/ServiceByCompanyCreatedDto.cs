using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Attributes;
using AgendaUnit.Shared.Queries;
using AutoMapper;

namespace AgendaUnit.Application.DTO.ServiceDto;

[AutoMap(typeof(Service), ReverseMap = true)]
public class ServiceByCompanyCreatedDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public TimeSpan Duration { get; set; }

    public decimal Price { get; set; }
}
