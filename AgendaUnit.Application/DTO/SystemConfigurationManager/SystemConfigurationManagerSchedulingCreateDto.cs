using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SystemConfigurationManager;

public class SystemConfigurationManagerSchedulingCreateDto
{
    public virtual SchedulingDto Scheduling { get; set; }

    public virtual CustomerDto Customer { get; set; }

    [AutoMap(typeof(Customer), ReverseMap = true)]
    public class CustomerDto
    {

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }


    }


    [AutoMap(typeof(Scheduling), ReverseMap = true)]
    public class SchedulingDto
    {
        public DateTime Date { get; set; }

        public string Duration { get; set; } // ISO 8601

        public decimal? TotalPrice { get; set; }

    }
}

