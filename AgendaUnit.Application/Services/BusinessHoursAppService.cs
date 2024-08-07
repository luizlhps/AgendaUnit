using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AutoMapper;


namespace AgendaUnit.Application.Services;
public class BusinessHoursAppService : Crud<BusinessHours, IBusinessHoursRepository, IBusinessHoursService>, IBusinessHoursAppService
{
    public BusinessHoursAppService(IBusinessHoursRepository repository, IMapper mapper, IBusinessHoursService baseService, IServiceProvider serviceProvider) : base(repository, mapper, baseService, serviceProvider)
    {
    }

}
