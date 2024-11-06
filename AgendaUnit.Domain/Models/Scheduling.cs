using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaUnit.Domain.Models
{
    public class Scheduling : BaseEntity
    {
        public int StatusId { get; set; }
        public int StaffUserId { get; set; }
        public int CompanyId { get; set; }
        public int CustomerId { get; set; }

        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public TimeSpan Duration { get; set; }
        public string? CancelNote { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Status Status { get; set; }
        public virtual Company Company { get; set; }
        public virtual Customer Customer { get; set; }
        public List<SchedulingService> SchedulingServices { get; set; }
        public virtual User User { get; set; }
    }
}
