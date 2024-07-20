using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.UserDto;

[AutoMap(typeof(User), ReverseMap = true)]
public class UserObtainDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}
