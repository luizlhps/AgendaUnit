using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Attributes;
using AgendaUnit.Shared.Queries;
using AgendaUnit.Shared.Queries.Interface;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SchedulingDto;

[AutoMap(typeof(Scheduling), ReverseMap = true)]
public class SchedulingListDto : QueryParams
{
    public int? Id { get; set; }
    public int? StatusId { get; set; }
    public int? StaffUserId { get; set; }
    public int? CompanyId { get; set; }
    public int? CustomerId { get; set; }

    public DateTime? Date { get; set; }

    [DateRange(nameof(SchedulingListDto.Date))]
    public DateTime? StartDate { get; set; }


    [DateRange(nameof(SchedulingListDto.Date))]
    public DateTime? EndDate { get; set; }


    public string? Notes { get; set; }
    public TimeSpan? Duration { get; set; }
    public string? CancelNote { get; set; }
    public decimal? TotalPrice { get; set; }
    public bool? IsDeleted { get; set; }

    //public virtual Status Status { get; set; }
    // virtual Company Company { get; set; }
    //public virtual Customer Customer { get; set; }
    public List<SchedulingService>? SchedulingServices { get; set; }

    [AutoMap(typeof(Scheduling), ReverseMap = true)]
    class SchedulingServiceDto
    {
        public int? Id { get; set; }
        public int? ServiceId { get; set; }
        public int? SchedulingId { get; set; }

        public string Name { get; set; }
        public double? Price { get; set; }
        public double? TotalPrice { get; set; }
        public double? Discount { get; set; }
        public bool? IsDeleted { get; set; }

        //public virtual Service Service { get; set; }
    }


}
