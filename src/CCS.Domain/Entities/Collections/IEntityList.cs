namespace CCS.Domain.Entities.Collections;

public interface IEntityList<TEntity> : ICollection<TEntity> where TEntity : Entity
{
    bool Contains(Guid id);
    TEntity? GetByIdOrDefault(Guid id);
    TEntity GetById(Guid id);
    TEntity this[Guid id] { get; }
}
