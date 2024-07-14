using AgendaUnit.Application.Services;
using AgendaUnit.Domain.Interfaces.Models;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.Models;
using AgendaUnit.Shared.Exceptions;
using AgendaUnit.Shared.Queries;
using AutoMapper;

namespace AgendaUnit.Domain.Services
{
    public class Crud<TEntity, TRepository, TBaseService> : ICrudAppService<TEntity>
        where TEntity : class, IBaseEntity, new()
        where TBaseService : IBaseService<TEntity, TRepository>
        where TRepository : IBaseRepository<TEntity>
    {
        private readonly TBaseService _baseService;
        private readonly TRepository _repository;
        private readonly IMapper _mapper;

        public Crud(TRepository repository, IMapper mapper, TBaseService baseService)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = baseService;
        }

        async public Task<TOutputDto> Create<TInputDto, TOutputDto>(TInputDto inputDto)
             where TInputDto : class
             where TOutputDto : class
        {
            var entity = _mapper.Map<TEntity>(inputDto);

            var createdEntity = await _baseService.Create(entity);

            return _mapper.Map<TOutputDto>(createdEntity);
        }

        async public Task<TOutputDto> Delete<TInputDto, TOutputDto>(TInputDto inputDto)
            where TInputDto : class
            where TOutputDto : class
        {
            var entity = _mapper.Map<TEntity>(inputDto);

            var deletedEntity = await _baseService.Delete(entity.Id);

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
                    throw new EntityNotFoundException($"{id} is not found");
                }

                return _mapper.Map<TOutputDto>(entity);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        async public Task<TOutputDto> Update<TInputDto, TOutputDto>(TInputDto inputDto)
             where TInputDto : class
             where TOutputDto : class
        {

            var entity = await _baseService.GetById(_mapper.Map<TEntity>(inputDto).Id);
            var entityMapper = _mapper.Map(inputDto, entity);

            var entityUpdated = await _baseService.Update(entityMapper);
            return _mapper.Map<TOutputDto>(entityUpdated);
        }

    }



}
