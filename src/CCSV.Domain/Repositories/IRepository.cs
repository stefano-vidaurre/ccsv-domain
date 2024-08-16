using CCSV.Domain.Entities;

namespace CCSV.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<int> Count(bool deletionsIncluded = false);
    Task<int> Count(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, bool deletionsIncluded = false);
    Task<bool> Any(bool deletionsIncluded = false);
    Task<bool> Any(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, bool deletionsIncluded = false);
    Task<TEntity?> FindOrDefault(Guid id);
    Task<TEntity> Find(Guid id);
    Task<TEntity?> GetByIdOrDefault(Guid id);
    Task<TEntity> GetById(Guid id);
    Task<IEnumerable<TEntity>> GetAll(bool deletionsIncluded = false);
    Task<IEnumerable<TEntity>> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>>query, bool deletionsIncluded = false);
    Task Create(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(TEntity entity);
}
