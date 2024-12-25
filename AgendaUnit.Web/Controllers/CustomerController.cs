using AgendaUnit.Application.DTO.CustomerDto;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Shared.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaUnit.Web.Controllers;

[ApiController]
[Route("customer")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerAppService _customerAppService;

    public CustomerController(ICustomerAppService CustomerAppService)
    {

        _customerAppService = CustomerAppService;
    }


    [Authorize]
    [HttpGet]
    async public Task<ActionResult> GetAll([FromQuery] CustomerListDto customerListDto)
    {
        return Ok(await _customerAppService.GetAllCustomersByCompany(customerListDto));
    }

}
