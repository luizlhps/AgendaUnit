using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.models;

namespace AgendaUnit.Domain.Services;

public class CustomerService : BaseService<Customer, ICustomerRepository>, ICustomerService
{
    public readonly ICustomerRepository _customerRepository;
    public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
    {
        _customerRepository = customerRepository;
    }

}
