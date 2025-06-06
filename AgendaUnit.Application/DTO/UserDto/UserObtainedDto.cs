using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.UserDto;

[AutoMap(typeof(User), ReverseMap = true)]
public class UserObtainedDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
    public string? RecoveryToken { get; set; }
    public string Phone { get; set; }
    public int? CompanyId { get; set; }
    public string RefreshToken { get; set; }
    public DateTimeOffset RefreshTokenExpiryTime { get; set; }
    [AutoMap(typeof(Company), ReverseMap = true)]
    public class UserObtainedCompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public UserObtainedCompanyDto Company { get; set; }
}
