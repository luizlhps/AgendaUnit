using AgendaUnit.Application.DTO;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Application.Services;
using AgendaUnit.Domain.Exceptions;
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

    [HttpGet("{id:int}")]
    async public Task<ActionResult<UserDto>> GetbyId(int id)
    {
        try
        {
            return Ok(await _userAppService.GetById<UserDto>(id));

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
