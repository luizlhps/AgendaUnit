using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.models;
using AgendaUnit.Infra.Context;
using AgendaUnit.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AgendaUnit.Infra.Repository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _context = appDbContext;
    }
    public async Task<User> GetByIdWithCompany(int id)
    {
        return await _context.User.Include(u => u.Company).FirstOrDefaultAsync(u => u.Id == id);
    }
}
