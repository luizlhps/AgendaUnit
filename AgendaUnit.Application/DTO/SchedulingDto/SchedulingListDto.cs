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


}
