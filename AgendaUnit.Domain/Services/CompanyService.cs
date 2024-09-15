using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;

namespace AgendaUnit.Domain.Services;

public class CompanyService : BaseService<Company>, ICompanyService
{
    public CompanyService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
