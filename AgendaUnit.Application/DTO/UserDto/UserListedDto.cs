using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.UserDto;

[AutoMap(typeof(User), ReverseMap = true)]
public class UserListedDto
{
    public string Name { get; set; }

    public RoleDto Role { get; set; }

    [AutoMap(typeof(Role), ReverseMap = true)]
    public class RoleDto
    {
        public string Name { get; set; }
    }

}
