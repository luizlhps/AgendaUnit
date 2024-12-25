using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Attributes;
using AgendaUnit.Shared.Queries;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SchedulingDto;

[AutoMap(typeof(Scheduling), ReverseMap = true)]
public class SchedulingByCompanyCreateDto
{
    public int StaffUserId { get; set; }
    public int CustomerId { get; set; }


    public DateTimeOffset Date { get; set; }
    public string? Notes { get; set; }
    public string Duration { get; set; } // ISO
    public string? CancelNote { get; set; }
    public double Discount { get; set; }


    public List<ServiceDto> Services { get; set; }

    [AutoMap(typeof(Service), ReverseMap = true)]
    public class ServiceDto
    {
        public int Id { get; set; }
    }




}
