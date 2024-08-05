using AgendaUnit.Application.DTO;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AgendaUnit.Shared.Exceptions;
using AutoMapper;

namespace AgendaUnit.Application.Services;

public class UserAppService : Crud<User, IUserRepository, IUserService>, IUserAppService
{
    private readonly IUserService _baseService;
    private readonly IMapper _mapper;

    public UserAppService(IUserRepository repository, IMapper mapper, IUserService baseService, IServiceProvider serviceProvider) : base(repository, mapper, baseService, serviceProvider)
    {
        _baseService = baseService;
        _mapper = mapper;
    }

    async public Task<TOutputDto> GetByIdWithCompany<TOutputDto>(int id) where TOutputDto : class
    {
        var user = await _baseService.GetByIdWithCompany(id);

        if (user == null)
        {
            throw new NotFoundException($"{id} is not found");
        }

        return _mapper.Map<TOutputDto>(user);
    }

    async public Task<UserCreatedDto> Register(UserCreateDto userCreateDto)
    {
        userCreateDto.Password = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);

        return await Create<UserCreateDto, UserCreatedDto>(userCreateDto);

    }

}
