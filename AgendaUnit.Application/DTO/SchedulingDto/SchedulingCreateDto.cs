using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SchedulingDto;


[AutoMap(typeof(Scheduling), ReverseMap = true)]
public class SchedulingCreateDto
{
    public DateTime Date { get; set; }

    public int StatusId { get; set; }

    public decimal? TotalPrice { get; set; }

    public int StaffUserId { get; set; }

    public int ServiceId { get; set; }

    public int CompanyId { get; set; }

    public int CustomerId { get; set; }

    public TimeSpan Duration { get; set; }

    public bool IsDeleted { get; set; }
}
