using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SchedulingDto;

[AutoMap(typeof(Scheduling), ReverseMap = true)]
public class SchedulingByCompanyUpdateDto
{
    public int Id { get; set; }
    public int StaffUserId { get; set; }
    public int CustomerId { get; set; }


    public DateTimeOffset Date { get; set; }
    public string? Notes { get; set; }
    public string Duration { get; set; } // ISO
    public double Discount { get; set; }


    public List<ServiceDto> Services { get; set; }
    public List<ServiceDto> OldServices { get; set; }

    [AutoMap(typeof(Service), ReverseMap = true)]
    public class ServiceDto
    {
        public int Id { get; set; }
    }
}