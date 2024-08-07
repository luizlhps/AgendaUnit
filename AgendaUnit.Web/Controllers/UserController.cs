using System.IdentityModel.Tokens.Jwt;
using AgendaUnit.Application.DTO;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Interfaces;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Application.Services;
using AgendaUnit.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaUnit.Web.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserAppService _userAppService;
    private readonly IUserMemoryCacheService _userMemoryCache;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserController(IUserAppService userAppService, IUserMemoryCacheService userMemoryCache, IHttpContextAccessor httpContextAccessor)
    {
        _userAppService = userAppService;
        _userMemoryCache = userMemoryCache;
        _httpContextAccessor = httpContextAccessor;

    }

    [Authorize]
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}", Name = "GetUserById")]
    async public Task<ActionResult<UserObtainedDto>> GetbyId(int id)
    {
        return Ok(await _userAppService.GetByIdWithCompany<UserObtainedDto>(id));
    }
    [HttpGet]
    async public Task<ActionResult<UserListedDto>> GetAll([FromQuery] UserListDto userListDto)
    {

        return Ok(await _userAppService.GetAll<UserListDto, UserListedDto>(userListDto));

    }


    [HttpPost]
    async public Task<ActionResult<UserCreatedDto>> Register(UserCreateDto userCreateDto)
    {

        var userCreatedDto = await _userAppService.Register(userCreateDto);
        return CreatedAtAction(nameof(GetbyId), new { id = userCreatedDto.Id }, userCreatedDto);

    }
}
