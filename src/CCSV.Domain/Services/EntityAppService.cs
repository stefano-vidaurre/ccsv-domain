using CCSV.Domain.Dtos;
using CCSV.Domain.Entities;
using CCSV.Domain.Mappers;
using CCSV.Domain.Parsers;
using CCSV.Domain.Repositories;

namespace CCSV.Domain.Services;

public abstract class EntityAppService<TEntity, TRead, TCreate, TUpdate, TQuery, TFilter> : IEntityAppService<TRead, TCreate, TUpdate, TQuery, TFilter>
    where TEntity : Entity
    where TRead : EntityReadDto
    where TCreate : EntityCreateDto
    where TUpdate : EntityUpdateDto
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
        IEnumerable<TEntity> entities = await _repository.GetAll(query =>
        {
            query = ApplyFilter(query, filter);
            if (filter.EntityCreationDateGreaterThan is not null)
            {
                DateTime greaterThanDate = DateTimeParser.ParseUTC(filter.EntityCreationDateGreaterThan);
                query = query.Where(x => x.EntityCreationDate >= greaterThanDate);
            }

            if (filter.EntityCreationDateLessThan is not null)
            {
                DateTime lessThanDate = DateTimeParser.ParseUTC(filter.EntityCreationDateLessThan);
                query = query.Where(x => x.EntityCreationDate <= lessThanDate);
            }

            return query
                .OrderBy(x => x.EntityCreationDate)
                .Skip(filter.Index * filter.Size)
                .Take(filter.Size);
        }, filter.DisabledIncluded);
        return _mapper.Map<IEnumerable<TEntity>, IEnumerable<TQuery>>(entities);
    }

    protected abstract IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, TFilter filter);

    public virtual async Task<TRead> GetById(Guid id)
    {
        TEntity entity = await _repository.GetById(id);
        return _mapper.Map<TEntity, TRead>(entity);
    }

    public Task<int> GetLength()
    {
        return _repository.Count();
    }

    public abstract Task Create(TCreate data);

    public abstract Task Update(TUpdate data);

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
