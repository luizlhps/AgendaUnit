using System;
using System.ComponentModel.DataAnnotations.Schema;
using AgendaUnit.Domain.Interfaces.Models;

namespace AgendaUnit.Domain.Models
{
    public class SchedulingService : BaseEntity
    {
        public int ServiceId { get; set; }
        public int SchedulingId { get; set; }

        public string Name { get; set; }

        public TimeSpan Duration { get; set; }
        public double Price { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Service Service { get; set; }
        public virtual Scheduling Scheduling { get; set; }

    }
}
