
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace AgendaUnit.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _appDbContext;

        public BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _appDbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _appDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Add(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var existingEntity = await _appDbContext.Set<TEntity>().FindAsync(entity); //fix it
            if (existingEntity == null)
                throw new InvalidOperationException("Entity not found");

            _appDbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _appDbContext.SaveChangesAsync();
            return existingEntity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _appDbContext.Set<TEntity>().FindAsync(id);
            if (entity == null)
                return false;

            _appDbContext.Set<TEntity>().Remove(entity);
            await _appDbContext.SaveChangesAsync();
            return true;
        }


    }
}
