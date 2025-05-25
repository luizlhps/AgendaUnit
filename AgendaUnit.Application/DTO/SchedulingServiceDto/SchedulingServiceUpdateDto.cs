using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SchedulingServiceDto;


[AutoMap(typeof(SchedulingService), ReverseMap = true)]
public class SchedulingServiceCreateDto
{
    public int ServiceId { get; set; }
    public int SchedulingId { get; set; }

    public TimeSpan Duration { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
}
