using AgendaUnit.Application.Services;
using AgendaUnit.Domain.Exceptions;
using AgendaUnit.Domain.Interfaces.Models;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Interfaces.Services;
using AgendaUnit.Domain.models;
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

        async public Task<IEnumerable<TOutputDto>> GetAll<TOutputDto>()
             where TOutputDto : class
        {
            var entites = await _baseService.GetAll();

            return entites.Select(e => _mapper.Map<TOutputDto>(e));
        }

        async public Task<TOutputDto> GetById<TOutputDto>(int id)
            where TOutputDto : class
        {
            try
            {
                var entity = await _baseService.GetById(id);

                if (entity == null)
                {
                    throw new EntityNotFoundException(id, "sdsd");
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
