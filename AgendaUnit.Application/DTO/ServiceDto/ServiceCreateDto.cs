using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Queries;
using AutoMapper;

namespace AgendaUnit.Application.DTO.ServiceDto;

[AutoMap(typeof(Service), ReverseMap = true)]
public class ServiceCreateDto
{
    public string Name { get; set; }

    public TimeSpan Duration { get; set; }

    public decimal Price { get; set; }

    public int StatusId { get; set; }

    public int CompanyId { get; set; }
}
