using System.Collections;
using CCS.Domain.Exceptions;

namespace CCS.Domain.Entities.Collections;

public class EntityList<TEntity> : IEntityList<TEntity> where TEntity : Entity
{
    private readonly Dictionary<Guid, TEntity> _items;

    public EntityList()
    {
        _items = new Dictionary<Guid, TEntity>();
    }

    public EntityList(IEnumerable<TEntity> items)
    {
        _items = new Dictionary<Guid, TEntity>();

        foreach (TEntity item in items)
        {
            _items.Add(item.Id, item);
        }
    }

    public virtual TEntity this[Guid id] => GetById(id);

    public virtual int Count => _items.Count;

    public virtual bool IsReadOnly => false;

    public virtual void Add(TEntity item)
    {
        if (Contains(item))
        {
            throw new WrongOperationException("The item already belongs to the list.");
        }

        _items.Add(item.Id, item);
    }

    public virtual void Clear()
    {
        _items.Clear();
    }

    public virtual bool Contains(TEntity item)
    {
        return _items.ContainsKey(item.Id);
    }

    public virtual bool Contains(Guid id)
    {
        return _items.ContainsKey(id);
    }

    public virtual void CopyTo(TEntity[] array, int arrayIndex)
    {
        _items.Values.CopyTo(array, arrayIndex);
    }

    public virtual TEntity GetById(Guid id)
    {
        TEntity? entity = GetByIdOrDefault(id);

        if (entity is null)
        {
            throw new WrongOperationException("The item doesn't belong to the list.");
        }

        return entity;
    }

    public virtual TEntity? GetByIdOrDefault(Guid id)
    {
        if (!_items.TryGetValue(id, out TEntity? entity))
        {
            return default;
        }

        return entity;
    }

    public virtual IEnumerator<TEntity> GetEnumerator()
    {
        return new EntityEnumerator<TEntity>(_items.Values);
    }

    public virtual bool Remove(TEntity item)
    {
        if (!_items.Remove(item.Id))
        {
            return false;
        }

        return true;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
