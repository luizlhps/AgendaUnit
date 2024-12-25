using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SchedulingDto;


[AutoMap(typeof(Scheduling), ReverseMap = true)]
public class SchedulingTotalValueUpdateDto
{
    public int Id { get; set; }
    public double TotalPrice { get; set; }
}
