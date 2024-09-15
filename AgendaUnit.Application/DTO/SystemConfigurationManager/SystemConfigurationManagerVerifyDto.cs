using AgendaUnit.Domain.Models;
using AutoMapper;

namespace AgendaUnit.Application.DTO.SystemConfigurationManager;

public class SystemConfigurationManagerVerifyDto
{
    public virtual UserDto User { get; set; }

    [AutoMap(typeof(User), ReverseMap = true)]
    public class UserDto
    {
        public int Id { get; set; }
    }
}
