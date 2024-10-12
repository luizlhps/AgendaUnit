using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaUnit.Domain.Models
{
    public class Scheduling : BaseEntity
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Notes { get; set; }

        public int StatusId { get; set; }

        public string CancelNote { get; set; }

        public decimal? TotalPrice { get; set; }

        public int StaffUserId { get; set; }

        public int ServiceId { get; set; }

        public int CompanyId { get; set; }

        public int CustomerId { get; set; }

        public bool IsDeleted { get; set; }

        public TimeSpan Duration { get; set; }

        public virtual Status Status { get; set; }

        public virtual Company Company { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Service Service { get; set; }

        public virtual User User { get; set; }
    }
}
