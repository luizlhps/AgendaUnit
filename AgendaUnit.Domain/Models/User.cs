using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AgendaUnit.Domain.models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public long Role { get; set; }

        [Column(name: "recovery_token")]
        public string RecoveryToken { get; set; }

        public string Phone { get; set; }

        public string Status { get; set; }

        [Column(name: "company_id")]
        public int? CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
