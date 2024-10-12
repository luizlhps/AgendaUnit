using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;

namespace AgendaUnit.Infra.Context;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly Dictionary<Type, object> _repositories = new();
    private IDbContextTransaction _transaction;

    public UnitOfWork(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _transaction.CommitAsync();
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();

    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }

    public async Task Dispose()
    {
        await _context.DisposeAsync();
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


