using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.models;

namespace AgendaUnit.Domain.Interfaces.Services;

public interface IServiceService : IBaseService<Service, IServiceRepository>
{
}
