using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Infrastructure.Repositories;
using AutoMapper;

namespace AgendaUnit.Infra.Context;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Commit()
    {
        _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IBaseRepository<TEntity> BaseRepository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            var repository = new BaseRepository<TEntity>(_context, _mapper);

            _repositories.Add(type, repository);
        }

        return (BaseRepository<TEntity>)_repositories[type];
    }

}


