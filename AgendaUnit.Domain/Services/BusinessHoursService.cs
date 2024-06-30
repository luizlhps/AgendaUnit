using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.models;

namespace AgendaUnit.Domain.Services;

public class BusinessHoursService : BaseService<BusinessHours, IBusinessHoursRepository>, IBusinessHoursService
{
    public readonly IBusinessHoursRepository _businessHoursRepository;
    public BusinessHoursService(IBusinessHoursRepository businessHoursRepository) : base(businessHoursRepository)
    {
        _businessHoursRepository = businessHoursRepository;
    }

}
