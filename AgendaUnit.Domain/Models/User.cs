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

        public string Role { get; set; }

        public string RecoveryToken { get; set; }

        public string Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Phone { get; set; }

        public string Status { get; set; }

        public int? CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
