using System;
using System.Collections.Generic;
using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Models;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Queries;
using AgendaUnit.Shared.Queries.Interface;

namespace AgendaUnit.Domain.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity>
          where TEntity : class, IBaseEntity, new()
    {
        //private readonly TRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            ///_repository = repository;
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await _unitOfWork.BaseRepository<TEntity>().GetById(id);

        }

        public virtual async Task<PageResult<TOutputDto>> GetAll<TInputDto, TOutputDto>(TInputDto inputDto)
        where TInputDto : QueryParams
        where TOutputDto : class
        {
            return await _unitOfWork.BaseRepository<TEntity>().GetAll<TInputDto, TOutputDto>(inputDto);
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            return await _unitOfWork.BaseRepository<TEntity>().Create(entity);
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            return await _unitOfWork.BaseRepository<TEntity>().Update(entity);
        }

        public virtual async Task<bool> Delete(int id)
        {
            return await _unitOfWork.BaseRepository<TEntity>().Delete(id);
        }
    }
}
