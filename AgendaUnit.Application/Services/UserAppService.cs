using AgendaUnit.Application.DTO;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AutoMapper;

namespace AgendaUnit.Application.Services;

public class UserAppService : Crud<User, IUserRepository, IUserService>, IUserAppService
{
    private readonly IUserService _baseService;
    private readonly IMapper _mapper;

    public UserAppService(IUserRepository repository, IMapper mapper, IUserService baseService) : base(repository, mapper, baseService)
    {
        _baseService = baseService;
        _mapper = mapper;
    }

    async public Task<TOutputDto> GetByIdWithCompany<TOutputDto>(int id) where TOutputDto : class
    {
        var user = await _baseService.GetByIdWithCompany(id);
        return _mapper.Map<TOutputDto>(user);
    }

}
