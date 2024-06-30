using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.models;

namespace AgendaUnit.Domain.Services;

public class ServiceService : BaseService<Service, IServiceRepository>, IServiceService
{
    public readonly IServiceRepository _serviceRepository;
    public ServiceService(IServiceRepository serviceRepository) : base(serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

}
