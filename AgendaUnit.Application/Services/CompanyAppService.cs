using AgendaUnit.Application.DTO;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Domain.Services;
using AutoMapper;

namespace AgendaUnit.Application.Services;

public class CompanyAppService : Crud<Company, ICompanyService>, ICompanyAppService
{
    public CompanyAppService(IUnitOfWork unitOfWork, IMapper mapper, ICompanyService baseService, IServiceProvider serviceProvider) : base(unitOfWork, mapper, baseService, serviceProvider)
    {
    }
}
