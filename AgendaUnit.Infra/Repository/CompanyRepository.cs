using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Models;
using AgendaUnit.Infra.Context;
using AgendaUnit.Infrastructure.Repositories;
using AutoMapper;

namespace AgendaUnit.Infra.Repository;

public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
{
    public CompanyRepository(AppDbContext appDbContext, IMapper mapper) : base(appDbContext, mapper)
    {
    }

}
