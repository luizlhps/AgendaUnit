using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Models;

namespace AgendaUnit.Domain.Interfaces.Services;

public interface IBusinessHoursService : IBaseService<BusinessHours, IBusinessHoursRepository>
{
}
