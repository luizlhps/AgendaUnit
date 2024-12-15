using System.Text.Json.Serialization;
using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.UserDto;

[AutoMap(typeof(User), ReverseMap = true)]
public class UserListedDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    public RoleDto Role { get; set; }
    public CompanyDto Company { get; set; }



    [AutoMap(typeof(Role), ReverseMap = true)]
    public class RoleDto
    {
        public string Name { get; set; }
    }


    [AutoMap(typeof(Company), ReverseMap = true)]
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
