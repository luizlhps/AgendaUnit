using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Attributes;
using AgendaUnit.Shared.Queries;
using AgendaUnit.Shared.Queries.Interface;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SchedulingServiceDto;

[AutoMap(typeof(SchedulingService), ReverseMap = true)]
public class SchedulingServiceListDto : QueryParams
{
    public int? Id { get; set; }

    public int? ServiceId { get; set; }
    public int? SchedulingId { get; set; }

    public string? Name { get; set; }
    public double? Price { get; set; }
    public double? TotalPrice { get; set; }
    public double? Discount { get; set; }
}
