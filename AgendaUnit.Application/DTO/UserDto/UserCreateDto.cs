using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.UserDto;

[AutoMap(typeof(User), ReverseMap = true)]
public class UserCreateDto
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int RoleId { get; set; }
    public string Phone { get; set; }
    public int? CompanyId { get; set; }

}
