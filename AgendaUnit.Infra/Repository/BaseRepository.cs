
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Infra.Context;
using AgendaUnit.Shared.Queries;
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


        public async Task<TEntity> Create(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Add(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var key = _appDbContext.Entry(entity).Property("Id").CurrentValue;

            var existingEntity = await _appDbContext.Set<TEntity>().FindAsync(key);

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

        async public Task<PageResult<TEntity>> GetAll<TInputDto, TOutputDto>(TInputDto inputDto)
        where TInputDto : QueryParams
        where TOutputDto : class
        {

            var pageResult = new PageResult<TEntity>
            {
                Items = [],
                PageNumber = 1,
                PageSize = 0,
                TotalCount = 0
            };

            IQueryable<TEntity> query = _appDbContext.Set<TEntity>();

            if (inputDto.Filters != null)
            {
                #region apply filter date of timestamp
                if (inputDto.Filters.StartDate.HasValue && inputDto.Filters.EndDate.HasValue)
                {
                    query = query.Where(item => EF.Property<DateTime>(item, "timestamp") >= inputDto.Filters.StartDate.Value
                                             && EF.Property<DateTime>(item, "timestamp") <= inputDto.Filters.EndDate.Value);
                }
                #endregion

            }

            #region apply filter search terms
            var baseProperties = typeof(TInputDto).BaseType.GetProperties();

            var dtoProperties = typeof(TInputDto).GetProperties()
            .Where(dtoProperties => !baseProperties.Any(baseProperties => baseProperties.Name == dtoProperties.Name));

            foreach (var property in dtoProperties)
            {
                var value = property.GetValue(inputDto, null);

                if (value != null)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        query = query.Where(item => EF.Property<string>(item, property.Name).Contains(value.ToString()));
                        continue;
                    }
                    if (property.PropertyType == typeof(bool))
                    {
                        query = query.Where(item => EF.Property<bool>(item, property.Name).Equals(value));
                        continue;
                    }
                }
            }

            #endregion

            #region apply include properties
            var dtoOutputProperties =
                typeof(TOutputDto).GetProperties();

            foreach (var property in dtoOutputProperties)
            {
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    var propertyName = property.Name;

                    query = query.Include(propertyName);
                }
            }


            #endregion

            #region apply pagination

            // return all items if allrows is true
            if (inputDto.PaginationProperties?.AllRows ?? false)
            {
                var items = await query.ToListAsync();

                pageResult = new PageResult<TEntity>
                {
                    Items = items,
                    PageNumber = 1,
                    PageSize = items.Count,
                    TotalCount = items.Count
                };
            }
            else
            {
                int totalItems = await query.CountAsync();
                int pageNumber = inputDto.PaginationProperties.PageNumber > 0 ? inputDto.PaginationProperties.PageNumber - 1 : 0;
                int pageSize = inputDto.PaginationProperties.PageSize > 0 ? inputDto.PaginationProperties.PageSize : 10;
                int totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

                if (pageNumber > totalPages)
                {
                    pageNumber = totalPages - 1;
                }

                query = query.Skip(pageNumber).Take(pageSize);

                var items = await query.ToListAsync();

                pageResult = new PageResult<TEntity>
                {
                    Items = items,
                    PageNumber = pageNumber + 1,
                    PageSize = pageSize,
                    TotalCount = totalItems
                };

            }
            #endregion

            Console.WriteLine(query);

            return pageResult;
        }



    }
}
