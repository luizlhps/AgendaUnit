using System.Text.Json.Serialization;
using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SchedulingDto;

[AutoMap(typeof(Scheduling), ReverseMap = true)]
public class SchedulingListedDto
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string? Notes { get; set; }

    public int StatusId { get; set; }

    public string? CancelNote { get; set; }

    public decimal? TotalPrice { get; set; }

    public int StaffUserId { get; set; }

    public int ServiceId { get; set; }

    public int CompanyId { get; set; }

    public int CustomerId { get; set; }

    public bool IsDeleted { get; set; }

}
