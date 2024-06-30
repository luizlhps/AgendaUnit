using AgendaUnit.Application.DTO;
using AgendaUnit.Application.Interfaces.Services;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.models;
using AgendaUnit.Domain.Services;
using AutoMapper;

namespace AgendaUnit.Application.Services;

public class CompanyAppService : Crud<Company, ICompanyRepository, ICompanyService>, ICompanyAppService
{
    public CompanyAppService(ICompanyRepository repository, IMapper mapper, ICompanyService baseService) : base(repository, mapper, baseService)
    {
    }

}
