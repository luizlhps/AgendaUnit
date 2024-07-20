
using AgendaUnit.Domain.Models;

namespace AgendaUnit.Application.DTO.UserDto;

public class UserDto  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public string? RecoveryToken { get; set; }
    public string Phone { get; set; }
    public int? CompanyId { get; set; }
    public Company Company { get; set; }
}

