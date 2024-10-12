
using System.Reflection;
using AgendaUnit.Domain.Interfaces.Repositories;
using AgendaUnit.Domain.Models;
using AgendaUnit.Infra.Context;
using AgendaUnit.Shared.Attributes;
using AgendaUnit.Shared.Exceptions;
using AgendaUnit.Shared.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AgendaUnit.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _appDbContext;
        protected readonly IMapper _mapper;

        public BaseRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _appDbContext.Set<TEntity>().FindAsync(id);
        }


        public async Task<TEntity> Create(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Add(entity);
            // await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var key = _appDbContext.Entry(entity).Property("Id").CurrentValue;

            var existingEntity = await _appDbContext.Set<TEntity>().FindAsync(key);

            if (existingEntity == null)
                throw new InvalidOperationException("Entity not found");

            _appDbContext.Entry(existingEntity).CurrentValues.SetValues(entity);

            // await _appDbContext.SaveChangesAsync();
            return existingEntity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _appDbContext.Set<TEntity>().FindAsync(id);
            if (entity == null)
                return false;

            _appDbContext.Set<TEntity>().Remove(entity);
            // await _appDbContext.SaveChangesAsync();
            return true;
        }

        async public Task<PageResult<TOutputDto>> GetAll<TInputDto, TOutputDto>(TInputDto inputDto)
        where TInputDto : QueryParams
        where TOutputDto : class
        {

            var pageResult = new PageResult<TOutputDto>
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


            var dateRangeProperty = dtoProperties.FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(DateRangeAttribute)));

            if (dateRangeProperty != null)
            {
                var dateRangeValue = (DateRangeAttribute)Attribute.GetCustomAttribute(dateRangeProperty, typeof(DateRangeAttribute));

                var dateStartProperty = typeof(TInputDto).GetProperty("StartDate");
                var dateEndProperty = typeof(TInputDto).GetProperty("EndDate");

                if (dateEndProperty != null && dateStartProperty != null)
                {
                    var startValue = (DateTime)dateStartProperty.GetValue(inputDto);
                    var endValue = (DateTime)dateEndProperty.GetValue(inputDto);

                    query = query.Where(item =>
                    EF.Property<DateTime>(item, dateRangeValue.ReferencedProperty) >= startValue &&
                    EF.Property<DateTime>(item, dateRangeValue.ReferencedProperty) <= endValue);

                }
            }




            foreach (var property in dtoProperties)
            {
                var value = property.GetValue(inputDto, null);

                if (value != null)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        var stringValue = value.ToString();

                        //attributes
                        var stringEqualsAttribute = property.GetCustomAttribute(typeof(StringEqualsAttribute));
                        var caseInsensitiveAttribute = property.GetCustomAttribute(typeof(CaseStringInsensitiveAttribute));

                        // string equals
                        if (stringEqualsAttribute != null)
                        {
                            query = query.Where(item =>
                                    (caseInsensitiveAttribute != null
                                          ? EF.Property<string>(item, property.Name).ToLower() == stringValue.ToLower()
                                          : EF.Property<string>(item, property.Name) == stringValue));
                        }
                        else
                        {
                            // contains %Value%
                            query = query.Where(item =>
                                    (caseInsensitiveAttribute != null
                                            ? EF.Property<string>(item, property.Name).ToLower().Contains(stringValue.ToLower())
                                            : EF.Property<string>(item, property.Name).Contains(stringValue)));

                        }

                        continue;
                    }
                    if (property.PropertyType == typeof(bool))
                    {
                        query = query.Where(item => EF.Property<bool>(item, property.Name).Equals(value));
                        continue;
                    }
                    if (property.PropertyType.IsValueType && !property.PropertyType.IsEnum && !property.Name.Equals("StartDate") && !property.Name.Equals("EndDate"))
                    {
                        if (property.PropertyType.Name.Equals("DateTime"))
                        {
                            if ((DateTime)value != DateTime.MinValue)
                            {
                                query = query.Where(item => EF.Property<DateTime>(item, property.Name).Date == ((DateTime)value).Date);
                            }

                            continue;
                        }

                        query = query.Where(item => EF.Property<object>(item, property.Name).Equals(value));
                        continue;
                    }
                    if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                    {
                        ApplyNestedPropertiesFilter(query, value, property.Name);
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


            #region entities Isdeleted
            query = query.Where(item => EF.Property<bool>(item, "IsDeleted") == false);

            #endregion


            #endregion

            #region apply pagination

            // return all items if allrows is true
            if (inputDto.PaginationProperties?.AllRows ?? false)
            {
                var items = await query.ProjectTo<TOutputDto>(_mapper.ConfigurationProvider).ToListAsync();

                pageResult = new PageResult<TOutputDto>
                {
                    Items = items,
                    PageNumber = 1,
                    PageSize = items.Count,
                    TotalCount = items.Count
                };
            }
            else
            {
                int totalItems = await query.CountAsync(); //TODO FIX IT
                int pageNumber = inputDto.PaginationProperties.PageNumber > 0 ? inputDto.PaginationProperties.PageNumber - 1 : 0;
                int pageSize = inputDto.PaginationProperties.PageSize > 0 ? inputDto.PaginationProperties.PageSize : 10;
                int totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

                if (pageNumber > totalPages)
                {
                    pageNumber = totalPages - 1;
                }

                var items = await query.ProjectTo<TOutputDto>(_mapper.ConfigurationProvider).Skip(pageNumber).Take(pageSize).ToListAsync();

                pageResult = new PageResult<TOutputDto>
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
        private void ApplyNestedPropertiesFilter<TEntity>(IQueryable<TEntity> query, object nestedObject, string parentPropertyName)
        {
            var nestedProperties = nestedObject.GetType().GetProperties();

            foreach (var nestedProperty in nestedProperties)
            {
                var nestedValue = nestedProperty.GetValue(nestedObject, null);

                if (nestedValue != null)
                {
                    if (nestedProperty.PropertyType == typeof(string))
                    {
                        query = query.Where(item => EF.Property<string>(item, nestedProperty.Name).Contains(nestedValue.ToString()));
                    }
                    if (nestedProperty.PropertyType == typeof(bool))
                    {
                        query = query.Where(item => EF.Property<string>(item, nestedProperty.Name).Contains(nestedValue.ToString()));
                    }
                    if (nestedProperty.PropertyType.IsValueType)
                    {
                        query = query.Where(item => EF.Property<string>(item, nestedProperty.Name).Contains(nestedValue.ToString()));
                    }
                    if (nestedProperty.PropertyType.IsClass && nestedProperty.PropertyType != typeof(string))
                    {
                        ApplyNestedPropertiesFilter(query, nestedValue, nestedProperty.Name);
                    }
                }
            }
        }


    }





}



