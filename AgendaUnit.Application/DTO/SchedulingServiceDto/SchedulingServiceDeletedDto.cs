using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Attributes;
using AgendaUnit.Shared.Queries;
using AgendaUnit.Shared.Queries.Interface;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SchedulingServiceDto;

[AutoMap(typeof(SchedulingService), ReverseMap = true)]
public class SchedulingServiceDeletedDto
{
    public int? Id { get; set; }

}
