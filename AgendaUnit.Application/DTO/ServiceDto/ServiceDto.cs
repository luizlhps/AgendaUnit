using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.ServiceDto;


[AutoMap(typeof(Service), ReverseMap = true)]
public class ServiceDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Duration { get; set; }
    public decimal Price { get; set; }
    public bool Ativo { get; set; }
    public int StatusId { get; set; }
    public Status Status { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}
