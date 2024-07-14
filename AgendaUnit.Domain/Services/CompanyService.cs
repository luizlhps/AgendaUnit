using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;

namespace AgendaUnit.Domain.Services;

public class CompanyService : BaseService<Company, ICompanyRepository>, ICompanyService
{
    public readonly ICompanyRepository _companyRepository;
    public CompanyService(ICompanyRepository companyRepository) : base(companyRepository)
    {
        _companyRepository = companyRepository;
    }

}
