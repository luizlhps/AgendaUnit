using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;

namespace AgendaUnit.Domain.Services;

public class ServiceService : BaseService<Service>, IServiceService
{
    public readonly IServiceRepository _serviceRepository;

    public ServiceService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
