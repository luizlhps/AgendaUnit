using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Queries;
using AutoMapper;

namespace AgendaUnit.Application.DTO.ServiceDto;

[AutoMap(typeof(Service), ReverseMap = true)]
public class ServiceUpdatedDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TimeSpan Duration { get; set; }
    public double Price { get; set; }
    public bool Ativo { get; set; }
    public int StatusId { get; set; }
}
