using AgendaUnit.Application.DTO;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Application.Services;
using AgendaUnit.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AgendaUnit.Web.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserAppService _userAppService;

    public UsersController(IUserAppService userAppService)
    {
        _userAppService = userAppService;
    }

    [HttpGet("{id:int}", Name = "GetUserById")]
    async public Task<ActionResult<UserObtainedDto>> GetbyId(int id)
    {
        return Ok(await _userAppService.GetByIdWithCompany<UserObtainedDto>(id));

    }
    [HttpGet]
    async public Task<ActionResult<UserListedDto>> GetAll([FromQuery] UserListDto userListDto)
    {
        try
        {
            return Ok(await _userAppService.GetAll<UserListDto, UserListedDto>(userListDto));

        }
        catch (EntityNotFoundException ex)
        {

            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            // Log a exceção ou trate outros cenários de erro
            return StatusCode(500, $"Internal Server Error {ex.Message}");
        }
    }


    [HttpPost]
    async public Task<ActionResult<UserCreatedDto>> CreateUser(UserCreateDto userCreateDto)
    {
        try
        {
            var userCreatedDto = await _userAppService.Create<UserCreateDto, UserCreatedDto>(userCreateDto);
            return CreatedAtAction(nameof(GetbyId), new { id = userCreatedDto.Id }, userCreatedDto);

        }
        catch (EntityNotFoundException ex)
        {

            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            // Log a exceção ou trate outros cenários de erro
            return StatusCode(500, $"Internal Server Error {ex.Message}");
        }
    }
}
