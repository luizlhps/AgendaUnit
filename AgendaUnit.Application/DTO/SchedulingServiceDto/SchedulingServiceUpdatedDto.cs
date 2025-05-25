using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SchedulingServiceDto;

[AutoMap(typeof(SchedulingService), ReverseMap = true)]
public class SchedulingServiceUpdatedDto
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public int SchedulingId { get; set; }

    public string Name { get; set; }
    public double Price { get; set; }
    public double TotalPrice { get; set; }
    public double Discount { get; set; }
}