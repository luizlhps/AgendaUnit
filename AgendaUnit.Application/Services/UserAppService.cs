using AgendaUnit.Application.DTO;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AgendaUnit.Shared.CrossCutting;
using AgendaUnit.Shared.Exceptions;
using AgendaUnit.Shared.Queries;
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

        #region Verifiy already exists

        var emailUserListDto = new UserListDto
        {
            Email = userCreateDto.Email.ToLower()
        };

        var emailUserListedDto = await GetAll<UserListDto, UserListedDto>(emailUserListDto);
        var emailAlreadyExists = emailUserListedDto.Items.FirstOrDefault();

        if (emailAlreadyExists != null)
        {
            throw new ConflictException("Já existe um usuário com esse email");
        }


        var usernameUserListDto = new UserListDto
        {
            Username = userCreateDto.Username.ToLower()
        };

        var usernameUserListedDto = await GetAll<UserListDto, UserListedDto>(usernameUserListDto);
        var usernameAlreadyExists = usernameUserListedDto.Items.FirstOrDefault();

        if (usernameAlreadyExists != null)
        {
            throw new ConflictException("Já existe um usuário com esse login");
        }
        #endregion

        return await Create<UserCreateDto, UserCreatedDto>(userCreateDto);
    }


    async public Task<UserObtainedDto> GetInfo()
    {
        var userId = _common.UserId;

        return await GetById<UserObtainedDto>(userId);

    }

    async public Task<PageResult<UserByCompanyListedDto>> GetAllByCompany(UserByCompanyListDto userByCompanyListDto)
    {

        UserListDto userListDto = new UserListDto
        {
            Email = userByCompanyListDto.Email,
            Filters = userByCompanyListDto.Filters,
            Name = userByCompanyListDto.Name,
            Username = userByCompanyListDto.Username,
            PaginationProperties = userByCompanyListDto.PaginationProperties,
            Company = new UserListDto.CompanyDto
            {
                Id = _common.CompanyId
            }

        };


        return await GetAll<UserListDto, UserByCompanyListedDto>(userListDto);
    }

}
