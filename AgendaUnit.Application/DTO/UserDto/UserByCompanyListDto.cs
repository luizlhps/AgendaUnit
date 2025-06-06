using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Attributes;
using AgendaUnit.Shared.Queries;
using AgendaUnit.Shared.Queries.Interface;
using AutoMapper;

namespace AgendaUnit.Application.DTO.UserDto;

[AutoMap(typeof(User), ReverseMap = true)]
public class UserByCompanyListDto : QueryParams
{
    public string? Name { get; set; }
    public string? Email { get; set; }

    [StringEquals]
    [CaseStringInsensitive]
    public string? Username { get; set; }

}
