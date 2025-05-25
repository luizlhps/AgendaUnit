using AgendaUnit.Application.DTO.AuthenticationManagerDto;
using AgendaUnit.Application.Services;
using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Models;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Exceptions;
using AgendaUnit.Shared.Queries;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaUnit.Domain.Services
{
    public class Crud<TEntity> : ICrudAppService<TEntity>
        where TEntity : class, IBaseEntity, new()
    {

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IServiceProvider _serviceProvider;

        public Crud(IUnitOfWork unitOfWork, IMapper mapper, IServiceProvider serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        async public Task<TOutputDto> Create<TInputDto, TOutputDto>(TInputDto inputDto, Boolean? isTransaction = null)
             where TInputDto : class
             where TOutputDto : class
        {
            Validate(inputDto);

            var entity = _mapper.Map<TEntity>(inputDto);

            var createdEntity = await CreateExec(entity);

            //save changes
            if (isTransaction == null)
            {
                await _unitOfWork.Commit();
            }

            return _mapper.Map<TOutputDto>(createdEntity);
        }

        async public virtual Task<TEntity> CreateExec(TEntity entity)
        {
            var createdEntity = await _unitOfWork.BaseRepository<TEntity>().Create(entity);

            return createdEntity;
        }

        async public Task<TOutputDto> Delete<TInputDto, TOutputDto>(TInputDto inputDto, Boolean? isTransaction = null)
            where TInputDto : class
            where TOutputDto : class
        {
            var entity = _mapper.Map<TEntity>(inputDto);

            var deletedEntity = await _unitOfWork.BaseRepository<TEntity>().Delete(entity);

            //save changes
            if (isTransaction == null)
            {
                await _unitOfWork.Commit();
            }

            return _mapper.Map<TOutputDto>(deletedEntity);
        }

        async public Task<PageResult<TOutputDto>> GetAll<TInputDto, TOutputDto>(TInputDto inputDto)
            where TInputDto : QueryParams
            where TOutputDto : class
        {

            return await _unitOfWork.BaseRepository<TEntity>().GetAll<TInputDto, TOutputDto>(inputDto);
        }

        async public Task<TOutputDto> GetById<TOutputDto>(int id)
            where TOutputDto : class
        {
            var entity = await _unitOfWork.BaseRepository<TEntity>().GetById(id);

            return _mapper.Map<TOutputDto>(entity);
        }

        async public Task<TOutputDto> Update<TInputDto, TOutputDto>(TInputDto inputDto, Boolean? isTransaction = null)
             where TInputDto : class
             where TOutputDto : class
        {
            Validate(inputDto);

            var idProperty = typeof(TInputDto).GetProperties()
            .FirstOrDefault(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));

            if (idProperty == null)
            {
                var modelState = new ModelStateDictionary();
                modelState.AddModelError("Id", "Id é obrigatório");

                throw new BadRequestException(modelState, "Id é obrigatório");
            }
            var idValue = idProperty.GetValue(inputDto);

            if (!(idValue is int id))
            {
                var modelState = new ModelStateDictionary();
                modelState.AddModelError("Id", "Id não do tipo int");

                throw new BadRequestException(modelState, "Id não do tipo int");
            }

            if (idValue.Equals(0))
            {
                var modelState = new ModelStateDictionary();
                modelState.AddModelError("Id", "O valor do id não pode ser zero");

                throw new BadRequestException(modelState, "O valor do id não pode ser zero");
            }

            var existingEntity = await _unitOfWork.BaseRepository<TEntity>().GetById(id);

            _mapper.Map(inputDto, existingEntity);

            var entityUpdated = await _unitOfWork.BaseRepository<TEntity>().Update(existingEntity);

            //save changes
            if (isTransaction == null)
            {
                await _unitOfWork.Commit();
            }


            return _mapper.Map<TOutputDto>(entityUpdated);
        }

        private void Validate<TInputDto>(TInputDto inputDto)
            where TInputDto : class
        {
            var validator = _serviceProvider.GetService<IValidator<TInputDto>>();

            if (validator != null)
            {
                var validationResult = validator.Validate(inputDto);

                var ModelState = new ModelStateDictionary();

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }

                    throw new BadRequestException(ModelState, "Erro na validação");
                }
            }
        }
    }

}
