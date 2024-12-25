using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Attributes;
using AgendaUnit.Shared.Queries;
using AutoMapper;

namespace AgendaUnit.Application.DTO.ServiceDto;

[AutoMap(typeof(Service), ReverseMap = true)]
public class ServiceByCompanyCreateDto
{
    public string Name { get; set; }

    public string Duration { get; set; } // ISO 8601 format

    public double Price { get; set; }
}
