using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SchedulingDto;


[AutoMap(typeof(Scheduling), ReverseMap = true)]
public class SchedulingTotalValueUpdatedDto
{
    public int StatusId { get; set; }
    public int StaffUserId { get; set; }
    public int CompanyId { get; set; }
    public int CustomerId { get; set; }

    public DateTimeOffset Date { get; set; }
    public string? Notes { get; set; }
    public TimeSpan Duration { get; set; }
    public string? CancelNote { get; set; }
    public double TotalPrice { get; set; }
    public double Discount { get; set; }
}
