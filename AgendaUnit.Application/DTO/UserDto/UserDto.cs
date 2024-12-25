
using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.UserDto;

[AutoMap(typeof(User), ReverseMap = true)]
public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public string? RecoveryToken { get; set; }
    public DateTimeOffset RecoveryExpiryTime { get; set; }

    public string Phone { get; set; }
    public int? CompanyId { get; set; }
    public Company Company { get; set; }
    public string RefreshToken { get; set; }
    public DateTimeOffset RefreshTokenExpiryTime { get; set; }
}

