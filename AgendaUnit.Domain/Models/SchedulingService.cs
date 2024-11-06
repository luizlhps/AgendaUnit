using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaUnit.Domain.Models
{
    public class SchedulingService
    {
        public int ServiceId { get; set; }
        public int SchedulingId { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public double Discount { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Service Service { get; set; }
        public virtual Scheduling Scheduling { get; set; }

    }
}
