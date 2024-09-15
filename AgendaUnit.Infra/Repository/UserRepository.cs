using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Models;
using AgendaUnit.Infra.Context;
using AgendaUnit.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgendaUnit.Infra.Repository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext appDbContext, IMapper mapper) : base(appDbContext, mapper)
    {
    }

    public async Task<User> GetByIdWithCompany(int id)
    {
        return await _appDbContext.User.Include(u => u.Company).FirstOrDefaultAsync(u => u.Id == id);
    }
}
