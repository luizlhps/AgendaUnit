using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Queries;
using AutoMapper;

namespace AgendaUnit.Application.DTO.ServiceDto;

[AutoMap(typeof(Service), ReverseMap = true)]
public class ServiceByCompanyUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Duration { get; set; } //ISO
    public double Price { get; set; }
}
