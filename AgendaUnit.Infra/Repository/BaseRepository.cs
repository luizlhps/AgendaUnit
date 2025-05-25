
using System.Linq.Expressions;
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

        public async virtual Task<TEntity> Create(TEntity entity)
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

        public async Task<TEntity> Delete(TEntity entity)
        {
            TEntity entityFinded = null;

            #region find entity

            var keyProperties = _appDbContext.Model
                .FindEntityType(typeof(TEntity))?
                .FindPrimaryKey()?
                .Properties;

            //composite key 
            if (keyProperties.Count > 1)
            {
                IQueryable<TEntity> query = _appDbContext.Set<TEntity>();

                foreach (var keyProperty in keyProperties)
                {
                    var keyValue = typeof(TEntity).GetProperty(keyProperty.Name)?.GetValue(entity);

                    query = query.Where(item => EF.Property<bool>(item, keyProperty.Name).Equals(keyValue));
                    continue;
                }

                entityFinded = await query.FirstAsync();
            }
            else
            {
                var keyName = keyProperties?.FirstOrDefault()?.Name;
                var keyValue = typeof(TEntity).GetProperty(keyName)?.GetValue(entity);

                if (keyValue == null || keyValue.Equals(0))
                {
                    throw new ConflictException($"A entidade não possui um valor válido para a chave '{keyName}'.");
                }

                entityFinded = await _appDbContext.Set<TEntity>().FindAsync(keyValue);
            }

            if (entityFinded == null)
                return null;

            #endregion


            var isDeletedProperty = typeof(TEntity).GetProperty("IsDeleted");

            if (isDeletedProperty != null)
            {
                // Soft delete
                isDeletedProperty.SetValue(entityFinded, true);
                _appDbContext.Entry(entityFinded).State = EntityState.Modified;
            }
            else
            {
                // Hard delete
                _appDbContext.Set<TEntity>().Remove(entityFinded);
            }
            // await _appDbContext.SaveChangesAsync();
            return entityFinded;
        }

        async public Task<PageResult<TOutputDto>> GetAll<TInputDto, TOutputDto>(TInputDto inputDto)
        where TInputDto : QueryParams
        where TOutputDto : class
        {
            var baseProperties = typeof(TInputDto).BaseType.GetProperties();

            var allProperties = typeof(TInputDto).GetProperties();

            var dtoProperties = allProperties
            .Where(dtoProperties => !baseProperties.Any(baseProperties => baseProperties.Name == dtoProperties.Name));


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
                    query = query.Where(item => EF.Property<DateTimeOffset>(item, "timestamp") >= inputDto.Filters.StartDate.Value
                                             && EF.Property<DateTimeOffset>(item, "timestamp") <= inputDto.Filters.EndDate.Value);
                }

                #endregion

            }

            #region apply filter search terms

            var dateRangeProperty = dtoProperties.FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(DateRangeAttribute)));

            if (dateRangeProperty != null)
            {
                var dateRangeValue = (DateRangeAttribute)Attribute.GetCustomAttribute(dateRangeProperty, typeof(DateRangeAttribute));

                var dateStartProperty = typeof(TInputDto).GetProperty("StartDate");
                var dateEndProperty = typeof(TInputDto).GetProperty("EndDate");

                if (dateEndProperty != null && dateStartProperty != null)
                {
                    var startValue = dateStartProperty.GetValue(inputDto, null);
                    var endValue = dateEndProperty.GetValue(inputDto, null);

                    if (startValue != null && endValue != null)
                    {

                        if (endValue is DateTimeOffset endDateTimeOffset)
                        {
                            endDateTimeOffset = endDateTimeOffset.ToUniversalTime();
                            endValue = endDateTimeOffset;
                        }

                        if (startValue is DateTimeOffset startDateTimeOffset)
                        {
                            startDateTimeOffset = startDateTimeOffset.ToUniversalTime();
                            startValue = startDateTimeOffset;
                        }

                        query = query.Where(item =>
                        EF.Property<DateTimeOffset>(item, dateRangeValue.ReferencedProperty) >= (DateTimeOffset)startValue &&
                        EF.Property<DateTimeOffset>(item, dateRangeValue.ReferencedProperty) <= (DateTimeOffset)endValue);

                    }
                }
            }


            foreach (var property in dtoProperties)
            {
                var value = property.GetValue(inputDto, null);

                if (value != null)
                {

                    //STRING VALUES
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

                    //BOOLEAN VALUES
                    if (property.PropertyType == typeof(bool))
                    {
                        query = query.Where(item => EF.Property<bool>(item, property.Name).Equals(value));
                        continue;
                    }

                    //DATE VALUES
                    if (property.PropertyType.IsValueType && !property.PropertyType.IsEnum && !property.Name.Equals("StartDate") && !property.Name.Equals("EndDate"))
                    {
                        if (property.PropertyType.Name.Equals("DateTimeOffset"))
                        {
                            if ((DateTimeOffset)value != DateTimeOffset.MinValue)
                            {
                                query = query.Where(item => EF.Property<DateTimeOffset>(item, property.Name).Date == ((DateTimeOffset)value).Date);
                            }

                            continue;
                        }

                        query = query.Where(item => EF.Property<object>(item, property.Name).Equals(value));
                        continue;
                    }

                    //CLASS NESTED
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


            //TODO: make sorting by using attributes 
            #region Sorting

            //ApplySorting(query)

            BuildSortingPaths(typeof(TInputDto));

            foreach (var property in allProperties)
            {
                var attribute = (SortableAttribute)Attribute.GetCustomAttribute(property, typeof(SortableAttribute));

                //CLASS NESTED
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    // ApplySortingNested(query, value, property.Name);
                    continue;
                }
                else
                {

                    if (attribute != null)
                    {
                        query = attribute.IsDescending
                        ? query.OrderByDescending(item => EF.Property<object>(item, property.Name))
                        : query.OrderBy(item => EF.Property<object>(item, property.Name));

                    }

                    continue;
                }
            }

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

                var items = await query.ProjectTo<TOutputDto>(_mapper.ConfigurationProvider).Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();

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
                        var stringValue = nestedValue.ToString();

                        //attributes
                        var stringEqualsAttribute = nestedProperty.GetCustomAttribute(typeof(StringEqualsAttribute));
                        var caseInsensitiveAttribute = nestedProperty.GetCustomAttribute(typeof(CaseStringInsensitiveAttribute));

                        // string equals
                        if (stringEqualsAttribute != null)
                        {
                            query = query.Where(item =>
                                    (caseInsensitiveAttribute != null
                                          ? EF.Property<string>(item, nestedProperty.Name).ToLower() == stringValue.ToLower()
                                          : EF.Property<string>(item, nestedProperty.Name) == stringValue));
                        }
                        else
                        {
                            // contains %Value%
                            query = query.Where(item =>
                                    (caseInsensitiveAttribute != null
                                            ? EF.Property<string>(item, nestedProperty.Name).ToLower().Contains(stringValue.ToLower())
                                            : EF.Property<string>(item, nestedProperty.Name).Contains(stringValue)));

                        }

                        continue;
                    }
                    if (nestedProperty.PropertyType == typeof(bool))
                    {
                        query = query.Where(item => EF.Property<bool>(item, nestedProperty.Name).Equals(nestedValue));
                        continue;
                    }
                    if (nestedProperty.PropertyType.IsValueType && !nestedProperty.PropertyType.IsEnum && !nestedProperty.Name.Equals("StartDate") && !nestedProperty.Name.Equals("EndDate"))
                    {
                        if (nestedProperty.PropertyType.Name.Equals("DateTimeOffset"))
                        {
                            if ((DateTimeOffset)nestedValue != DateTimeOffset.MinValue)
                            {
                                query = query.Where(item => EF.Property<DateTimeOffset>(item, nestedProperty.Name).Date == ((DateTimeOffset)nestedValue).Date);
                            }

                            continue;
                        }

                        query = query.Where(item => EF.Property<object>(item, nestedProperty.Name).Equals(nestedValue));
                        continue;
                    }
                    if (nestedProperty.PropertyType.IsClass && nestedProperty.PropertyType != typeof(string))
                    {
                        ApplyNestedPropertiesFilter(query, nestedValue, nestedProperty.Name);
                        continue;
                    }
                }
            }
        }

        public static IQueryable<T> ApplySorting<T>(IQueryable<T> query, string propertyPath, bool ascending = true)
        {
            var param = Expression.Parameter(typeof(T), "x");
            Expression property = param;

            // Navega pelas propriedades aninhadas
            foreach (var prop in propertyPath.Split('.'))
            {
                property = Expression.Property(property, prop);
            }

            // Cria a expressão lambda
            var lambda = Expression.Lambda(property, param);

            // Determina o método de ordenação
            var methodName = ascending ? "OrderBy" : "OrderByDescending";

            // Invoca o método OrderBy ou OrderByDescending
            var result = typeof(Queryable).GetMethods()
                .First(method => method.Name == methodName && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.Type)
                .Invoke(null, new object[] { query, lambda });

            return (IQueryable<T>)result;
        }

        public static List<string> BuildSortingPaths(Type type, string parentPath = "")
        {
            var sortingPaths = new List<string>();  // Lista para armazenar os caminhos de ordenação

            // Verifique se o tipo é válido
            if (type == null)
            {
                Console.WriteLine("O tipo passado é nulo!");
                return sortingPaths; // Retorna uma lista vazia
            }

            Console.WriteLine($"Processando o tipo: {type.Name}");

            var properties = type.GetProperties();

            if (properties.Length == 0)
            {
                Console.WriteLine("Não há propriedades para processar.");
            }

            foreach (var property in properties)
            {
                // Mostra a propriedade sendo processada
                Console.WriteLine($"Processando a propriedade: {property.Name}");

                var propertyPath = string.IsNullOrEmpty(parentPath)
                    ? property.Name
                    : $"{parentPath}.{property.Name}";

                // Verifica se a propriedade possui o atributo de sorting
                var sortAttribute = property.GetCustomAttribute<SortableAttribute>();
                if (sortAttribute != null)
                {
                    Console.WriteLine($"Propriedade ordenável encontrada: {propertyPath}");
                    sortingPaths.Add(propertyPath); // Adiciona o caminho à lista
                }

                // Se for uma classe complexa (e não string), busca recursivamente
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    Console.WriteLine($"Propriedade {property.Name} é uma classe. Descendo recursivamente...");
                    sortingPaths.AddRange(BuildSortingPaths(property.PropertyType, propertyPath)); // Adiciona os caminhos encontrados recursivamente
                }
            }

            return sortingPaths; // Retorna a lista com todos os caminhos de ordenação
        }
    }
}



