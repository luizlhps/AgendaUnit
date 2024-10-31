using AgendaUnit.Application.DTO;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AgendaUnit.Shared.Exceptions;
using AutoMapper;

namespace AgendaUnit.Application.Services;

public class UserAppService : Crud<User>, IUserAppService
{
    public UserAppService(IUnitOfWork unitOfWork, IMapper mapper, IServiceProvider serviceProvider) : base(unitOfWork, mapper, serviceProvider)
    {
    }

    async public Task<UserCreatedDto> Register(UserCreateDto userCreateDto)
    {
        userCreateDto.Password = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);
        userCreateDto.RoleId = (int)RoleEnum.Admin;

        return await Create<UserCreateDto, UserCreatedDto>(userCreateDto);

    }

}
