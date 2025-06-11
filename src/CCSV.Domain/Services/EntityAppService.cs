using CCSV.Domain.Dtos;
using CCSV.Domain.Entities;
using CCSV.Domain.Mappers;
using CCSV.Domain.Parsers;
using CCSV.Domain.Repositories;
using CCSV.Domain.Validators;

namespace CCSV.Domain.Services;

public abstract class EntityAppService<TEntity, TRead, TCreate, TUpdate, TQuery, TFilter> :
    EntityAppService<TEntity, TRead, TCreate, TQuery, TFilter>,
    IEntityAppService<TRead, TCreate, TUpdate, TQuery, TFilter>
        where TEntity : Entity
        where TRead : EntityReadDto
        where TCreate : EntityCreateDto
        where TUpdate : EntityUpdateDto
        where TQuery : EntityQueryDto
        where TFilter : EntityFilterDto
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMasterValidator _validator;

    protected EntityAppService(IRepository<TEntity> repository, IMasterMapper mapper, IMasterValidator validator) : base(repository, mapper, validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public virtual async Task Update(Guid id, TUpdate data)
    {
        _validator.Validate(data);
        TEntity entity = await _repository.GetById(id);
        UpdateEntity(entity, data);
    }

    protected abstract void UpdateEntity(TEntity entity, TUpdate data);
}

public abstract class EntityAppService<TEntity, TRead, TCreate, TQuery, TFilter> :
    EntityAppService<TEntity, TRead, TQuery, TFilter>,
    IEntityAppService<TRead, TCreate, TQuery, TFilter>
    where TEntity : Entity
    where TRead : EntityReadDto
    where TCreate : EntityCreateDto
    where TQuery : EntityQueryDto
    where TFilter : EntityFilterDto
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMasterValidator _validator;

    protected EntityAppService(IRepository<TEntity> repository, IMasterMapper mapper, IMasterValidator validator) : base(repository, mapper)
    {
        _repository = repository;
        _validator = validator;
    }

    public virtual async Task Create(TCreate data)
    {
        _validator.Validate(data);
        TEntity entity = CreateEntity(data);
        await _repository.Create(entity);
    }

    protected abstract TEntity CreateEntity(TCreate data);
}

public abstract class EntityAppService<TEntity, TRead, TQuery, TFilter> : IEntityAppService<TRead, TQuery, TFilter>
    where TEntity : Entity
    where TRead : EntityReadDto
    where TQuery : EntityQueryDto
    where TFilter : EntityFilterDto
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMasterMapper _mapper;

    protected EntityAppService(IRepository<TEntity> repository, IMasterMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public virtual async Task<IEnumerable<TQuery>> GetAll(TFilter filter)
    {
        IEnumerable<TEntity> entities = await _repository.GetAll(query => CreateQuery(query, filter), filter.DisabledIncluded);
        return _mapper.Map<IEnumerable<TEntity>, IEnumerable<TQuery>>(entities);
    }

    private IQueryable<TEntity> CreateQuery(IQueryable<TEntity> query, TFilter filter)
    {
        IQueryable<TEntity> queryWithFilters = ApplyFilter(query, filter);
        if (filter.EntityCreationDateGreaterThan is not null)
        {
            DateTime greaterThanDate = DateTimeParser.ParseUTC(filter.EntityCreationDateGreaterThan);
            queryWithFilters = query.Where(x => x.EntityCreationDate >= greaterThanDate);
        }

        if (filter.EntityCreationDateLessThan is not null)
        {
            DateTime lessThanDate = DateTimeParser.ParseUTC(filter.EntityCreationDateLessThan);
            queryWithFilters = query.Where(x => x.EntityCreationDate <= lessThanDate);
        }

        return queryWithFilters
            .OrderBy(x => x.EntityCreationDate)
            .Skip(filter.Index * filter.Size)
            .Take(filter.Size);
    }

    protected abstract IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, TFilter filter);

    public virtual async Task<TRead> GetById(Guid id)
    {
        TEntity entity = await _repository.GetById(id);
        return _mapper.Map<TEntity, TRead>(entity);
    }

    public virtual Task<int> GetLength(TFilter filter)
    {
        return _repository.Count(query => CreateQuery(query, filter), filter.DisabledIncluded);
    }

    public virtual async Task Delete(Guid id)
    {
        TEntity entity = await _repository.GetById(id);
        await _repository.Delete(entity);
    }

    public virtual async Task Enable(Guid id)
    {
        TEntity entity = await _repository.GetById(id);
        entity.SetAsEnabled();
    }

    public virtual async Task Disable(Guid id)
    {
        TEntity entity = await _repository.GetById(id);
        entity.SetAsDisabled();
    }
}
