using AgendaUnit.Application.DTO;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AutoMapper;

namespace AgendaUnit.Application.Services;

public class CustomerAppService : Crud<Customer, ICustomerService>, ICustomerAppService
{
    public CustomerAppService(IUnitOfWork unitOfWork, IMapper mapper, ICustomerService baseService, IServiceProvider serviceProvider) : base(unitOfWork, mapper, baseService, serviceProvider)
    {
    }
}
