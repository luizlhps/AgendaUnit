using AgendaUnit.Application.DTO.AuthenticationManagerDto;
using AgendaUnit.Application.Services;
using AgendaUnit.Domain.Interfaces.Context;
using AgendaUnit.Domain.Interfaces.Models;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Exceptions;
using AgendaUnit.Shared.Queries;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaUnit.Domain.Services
{
    public class Crud<TEntity, TBaseService> : ICrudAppService<TEntity>
        where TEntity : class, IBaseEntity, new()
        where TBaseService : IBaseService<TEntity>
    {

        protected readonly TBaseService _baseService;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IServiceProvider _serviceProvider;

        public Crud(IUnitOfWork unitOfWork, IMapper mapper, TBaseService baseService, IServiceProvider serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _baseService = baseService;
            _serviceProvider = serviceProvider;
        }

        async public Task<TOutputDto> Create<TInputDto, TOutputDto>(TInputDto inputDto, Boolean? isTransaction = null)
             where TInputDto : class
             where TOutputDto : class
        {
            Validate(inputDto);

            var entity = _mapper.Map<TEntity>(inputDto);

            var createdEntity = await _baseService.Create(entity);

            //save changes
            if (isTransaction == null)
            {
                await _unitOfWork.Commit();
            }

            return _mapper.Map<TOutputDto>(createdEntity);
        }

        async public Task<TOutputDto> Delete<TInputDto, TOutputDto>(TInputDto inputDto, Boolean? isTransaction = null)
            where TInputDto : class
            where TOutputDto : class
        {
            var entity = _mapper.Map<TEntity>(inputDto);

            var deletedEntity = await _baseService.Delete(entity.Id);

            //save changes
            if (isTransaction == null)
            {
                _unitOfWork.Commit();
            }

            return _mapper.Map<TOutputDto>(deletedEntity);
        }

        async public Task<PageResult<TOutputDto>> GetAll<TInputDto, TOutputDto>(TInputDto inputDto)
            where TInputDto : QueryParams
            where TOutputDto : class
        {

            var entities = await _baseService.GetAll<TInputDto, TOutputDto>(inputDto);

            var pageResult = new PageResult<TOutputDto>
            {
                Items = [],
                PageNumber = entities.PageNumber,
                PageSize = entities.PageSize,
                TotalCount = entities.TotalCount
            };

            if (entities.Items.Count > 0)
            {
                var teste = _mapper.Map<List<TOutputDto>>(entities.Items);
                pageResult.Items = teste;
            }

            return pageResult;
        }


        async public Task<TOutputDto> GetById<TOutputDto>(int id)
            where TOutputDto : class
        {
            try
            {
                var entity = await _baseService.GetById(id);

                if (entity == null)
                {
                    throw new NotFoundException($"{id} is not found");
                }

                return _mapper.Map<TOutputDto>(entity);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
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

            var existingEntity = await _baseService.GetById(id);

            _mapper.Map(inputDto, existingEntity);

            var entityUpdated = await _baseService.Update(existingEntity);

            //save changes
            if (isTransaction == null)
            {
                _unitOfWork.Commit();
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