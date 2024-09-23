using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SystemConfigurationManager;

public class SystemConfigurationManagerSchedulingCreateDto
{
    public virtual SchedulingDto Scheduling { get; set; }

    [AutoMap(typeof(Scheduling), ReverseMap = true)]
    public class SchedulingDto
    {
        public DateTime Date { get; set; }

        public TimeSpan Duration { get; set; }

        public decimal? TotalPrice { get; set; }

        public int ServiceId { get; set; }

        public int CustomerId { get; set; }
    }
}

