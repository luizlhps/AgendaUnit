using System.Text.Json.Serialization;
using AgendaUnit.Domain.models;

namespace AgendaUnit.Application.DTO;

public class UserDto
{
    public int Id { get; set; }

    public Guid Uuid { get; set; }
    public string Name { get; set; }

    public string Login { get; set; }

    public string Email { get; set; }

    [JsonIgnore]
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
