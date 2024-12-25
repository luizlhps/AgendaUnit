using AgendaUnit.Application.DTO;
using AgendaUnit.Application.DTO.CustomerDto;
using AgendaUnit.Application.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Queries;

namespace AgendaUnit.Application.Interfaces.Services;

public interface ICustomerAppService : ICrudAppService<Customer>
{
    Task<PageResult<CustomerListedDto>> GetAllCustomersByCompany(CustomerListDto customerListDto);

}
