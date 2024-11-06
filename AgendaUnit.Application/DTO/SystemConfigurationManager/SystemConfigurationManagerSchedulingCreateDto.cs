using System;
using System.Collections.Generic;
using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SystemConfigurationManager
{
    [AutoMap(typeof(Scheduling), ReverseMap = true)]
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
            public string Duration { get; set; } // ISO 8601 format
            public virtual SchedulingServiceDto SchedulingServices { get; set; } // Alterado para List

            [AutoMap(typeof(SchedulingService), ReverseMap = true)]
            public class SchedulingServiceDto
            {
                public string Name { get; set; }
                public double Price { get; set; }
                public double TotalPrice { get; set; }
                public double Discount { get; set; }
            }
        }
    }
}
