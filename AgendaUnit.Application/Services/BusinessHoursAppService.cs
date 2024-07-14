using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AutoMapper;

public class BusinessHoursAppService : Crud<BusinessHours, IBusinessHoursRepository, IBusinessHoursService>, IBusinessHoursAppService
{
    public BusinessHoursAppService(IBusinessHoursRepository repository, IMapper mapper, IBusinessHoursService baseService) : base(repository, mapper, baseService)
    {
    }

}
