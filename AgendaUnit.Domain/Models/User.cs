

using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaUnit.Domain.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }


        [Column(name: "role_id")]
        public int RoleId { get; set; }
        public Role Role { get; set; }


        [Column(name: "recovery_token")]
        public string? RecoveryToken { get; set; }


        [Column(name: "recovery_expiry_time")]
        public DateTimeOffset? RecoveryExpiryTime { get; set; }


        [Column(name: "refresh_token")]
        public string? RefreshToken { get; set; }


        [Column(name: "refresh_token_expiry_time")]
        public DateTimeOffset? RefreshTokenExpiryTime { get; set; }

        public string Phone { get; set; }


        [Column(name: "company_id")]
        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        public List<Scheduling> Schedulings { get; set; }
    }
}
