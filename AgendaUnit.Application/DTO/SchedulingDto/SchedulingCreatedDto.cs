using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SchedulingDto;

[AutoMap(typeof(Scheduling), ReverseMap = true)]
public class SchedulingCreatedDto
{
}
