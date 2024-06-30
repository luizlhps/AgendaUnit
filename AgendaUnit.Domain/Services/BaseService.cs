using System;
using System.Collections.Generic;
using AgendaUnit.Domain.Interfaces.Models;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.models;

namespace AgendaUnit.Domain.Services
{
    public class BaseService<TEntity, TRepository> : IBaseService<TEntity, TRepository>
          where TEntity : class, IBaseEntity, new()
          where TRepository : IBaseRepository<TEntity>
    {
        private readonly TRepository _repository;

        public BaseService(TRepository repository)
        {
            _repository = repository;
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await _repository.GetById(id);

        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _repository.GetAll();
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            return await _repository.Create(entity);
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            return await _repository.Update(entity);
        }

        public virtual async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}
