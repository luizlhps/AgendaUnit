using AgendaUnit.Application.DTO;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AgendaUnit.Shared.CrossCutting;
using AgendaUnit.Shared.Exceptions;
using AutoMapper;

namespace AgendaUnit.Application.Services;

public class UserAppService : Crud<User>, IUserAppService
{
    readonly private ICommon _common;


    public UserAppService(
        IUnitOfWork unitOfWork
        , IMapper mapper
        , IServiceProvider serviceProvider
        , ICommon common) : base(unitOfWork, mapper, serviceProvider)
    {
        _common = common;
    }

    async public Task<UserCreatedDto> Register(UserCreateDto userCreateDto)
    {
        userCreateDto.Password = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);
        userCreateDto.RoleId = (int)RoleEnum.Admin;

        return await Create<UserCreateDto, UserCreatedDto>(userCreateDto);
    }

    async public Task<UserObtainedDto> GetInfo()
    {
        var userId = _common.UserId;

        return await GetById<UserObtainedDto>(userId);

    }


}
