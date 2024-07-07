using System;
using System.ComponentModel.DataAnnotations.Schema;
using AgendaUnit.Domain.models;

namespace AgendaUnit.Domain.Models
{
    public class Scheduling : BaseEntity
    {
        public DateTime Date { get; set; }

        public string Hours { get; set; }

        public string Notes { get; set; }

        public int Status { get; set; }

        [Column(name: "cancel_note")]
        public string? CancelNote { get; set; }

        [Column(name: "total_price")]
        public decimal? TotalPrice { get; set; }

        [Column(name: "staff_user_id")]
        public int StaffUserId { get; set; }
        public User StaffUser { get; set; }

        [Column(name: "service_id")]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [Column(name: "company_id")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        // Corrigindo o relacionamento para Customer
        [Column(name: "customer_id")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
