using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Attributes;
using AgendaUnit.Shared.Queries;
using AgendaUnit.Shared.Queries.Interface;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SchedulingDto;

[AutoMap(typeof(Scheduling), ReverseMap = true)]
public class SchedulingListDto : QueryParams
{

    public DateTime Date { get; set; }

    [DateRange(nameof(SchedulingListDto.Date))]
    public DateTime? StartDate { get; set; }


    [DateRange(nameof(SchedulingListDto.Date))]
    public DateTime? EndDate { get; set; }

    public int? Id { get; set; }

    public int? StatusId { get; set; }

    public decimal? TotalPrice { get; set; }

    public int? StaffUserId { get; set; }

    public int? ServiceId { get; set; }

    public int? CompanyId { get; set; }

    public int? CustomerId { get; set; }

    public TimeSpan? Duration { get; set; }

}
