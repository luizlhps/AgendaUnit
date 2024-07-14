using System;
using System.Collections.Generic;
using AgendaUnit.Domain.Interfaces.Models;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Queries;
using AgendaUnit.Shared.Queries.Interface;

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

        public virtual async Task<PageResult<TEntity>> GetAll<TInputDto, TOutputDto>(TInputDto inputDto)
        where TInputDto : QueryParams
        where TOutputDto : class
        {
            return await _repository.GetAll<TInputDto, TOutputDto>(inputDto);
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
