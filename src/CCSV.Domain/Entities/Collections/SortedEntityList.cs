using System.Collections;
using CCSV.Domain.Exceptions;

namespace CCSV.Domain.Entities.Collections;

public class SortedEntityList<TSortedEntity> : ISortedEntityList<TSortedEntity> where TSortedEntity : SortedEntity
{
    private readonly IEntityList<TSortedEntity> _entities;
    private readonly SortedList<long, TSortedEntity> _items;

    public SortedEntityList()
    {
        _entities = new EntityList<TSortedEntity>();
        _items = new SortedList<long, TSortedEntity>();
    }

    public SortedEntityList(IEnumerable<TSortedEntity> entities)
    {
        _entities = new EntityList<TSortedEntity>();
        _items = new SortedList<long, TSortedEntity>();

        foreach (TSortedEntity entity in entities)
        {
            ((ICollection<TSortedEntity>)this).Add(entity);
        }
    }

    public TSortedEntity this[int index] => ElementAt(index);

    public int Count => _items.Count;

    public bool IsReadOnly => false;

    public TSortedEntity this[Guid id] => _entities[id];

    public void Add(TSortedEntity item)
    {
        if (Contains(item))
        {
            throw new WrongOperationException("The item already belongs to the list.");
        }

        TSortedEntity? last = ElementAtOrDefault(Count - 1);

        if (last is null)
        {
            item.SetWeight(0);
        }
        else
        {
            item.SetWeight(last.SortedEntityWeight + 1);
        }

        _entities.Add(item);
        _items.Add(item.SortedEntityWeight, item);
    }

    void ICollection<TSortedEntity>.Add(TSortedEntity item)
    {
        if (Contains(item))
        {
            throw new WrongOperationException("The item already belongs to the list.");
        }

        while (_items.ContainsKey(item.SortedEntityWeight))
        {
            item.SetWeight(item.SortedEntityWeight + 1);
        }

        _entities.Add(item);
        _items.Add(item.SortedEntityWeight, item);
    }

    public void Push(TSortedEntity item)
    {
        if (Contains(item))
        {
            throw new WrongOperationException("The item already belongs to the list.");
        }

        TSortedEntity? first = ElementAtOrDefault(0);

        if (first is null)
        {
            item.SetWeight(0);
        }
        else
        {
            item.SetWeight(first.SortedEntityWeight - 1);
        }

        _entities.Add(item);
        _items.Add(item.SortedEntityWeight, item);
    }

    public void Clear()
    {
        _entities.Clear();
        _items.Clear();
    }

    public bool Contains(TSortedEntity item)
    {
        return _entities.Contains(item);
    }

    public void CopyTo(TSortedEntity[] array, int arrayIndex)
    {
        _items.Values.CopyTo(array, arrayIndex);
    }

    public TSortedEntity ElementAt(int index)
    {
        TSortedEntity? item = ElementAtOrDefault(index);

        if (item is null)
        {
            throw new WrongOperationException("The index doesn't belong to the sorted list.");
        }

        return item;
    }

    public TSortedEntity? ElementAtOrDefault(int index)
    {
        if (index < 0)
        {
            return default;
        }

        if (index >= _items.Count)
        {
            return default;
        }

        long key = _items.Keys[index];
        return _items[key];
    }

    public int IndexOf(TSortedEntity item)
    {
        if (!Contains(item))
        {
            return -1;
        }

        return _items.IndexOfKey(item.SortedEntityWeight);
    }

    public IEnumerator<TSortedEntity> GetEnumerator()
    {
        return new SortedEntityEnumerator<TSortedEntity>(_items.Values);
    }

    public void Swap(TSortedEntity origin, TSortedEntity destiny)
    {
        if (!Contains(origin))
        {
            throw new WrongOperationException("The origin item doesn't belong to the list.");
        }

        if (!Contains(destiny))
        {
            throw new WrongOperationException("The destiny item doesn't belong to the list.");
        }

        long auxiliarWeight = origin.SortedEntityWeight;

        origin.SetWeight(destiny.SortedEntityWeight);
        destiny.SetWeight(auxiliarWeight);
        _items[origin.SortedEntityWeight] = origin;
        _items[destiny.SortedEntityWeight] = destiny;
    }

    public void Insert(TSortedEntity item, int index)
    {
        if (Contains(item))
        {
            throw new WrongOperationException("The item already belongs to the list.");
        }

        if (index < 0 || index > _items.Count)
        {
            throw new WrongOperationException($"Index ({index}) out of range [0..{_items.Count}].");
        }

        Add(item);

        for (int pivot = _items.Count - 2; pivot >= index; pivot--)
        {
            TSortedEntity prev = ElementAt(pivot);
            Swap(item, prev);
        }
    }

    public void Move(TSortedEntity item, int index)
    {
        if (!Contains(item))
        {
            throw new WrongOperationException("The item doesn't belong to the list.");
        }

        int pivot = IndexOf(item);

        if (pivot > index)
        {
            for (int prevIndex = pivot - 1; prevIndex >= index; prevIndex--)
            {
                TSortedEntity prev = ElementAt(prevIndex);
                Swap(item, prev);
            }
        }
        else if (pivot < index)
        {
            for (int nextIndex = pivot + 1; nextIndex <= index; nextIndex++)
            {
                TSortedEntity next = ElementAt(nextIndex);
                Swap(item, next);
            }
        }
    }

    public bool Remove(TSortedEntity item)
    {
        if (!Contains(item))
        {
            return false;
        }

        if (!_entities.Remove(item))
        {
            return false;
        }

        if (!_items.Remove(item.SortedEntityWeight))
        {
            _entities.Add(item);
            return false;
        }

        return true;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }

    public TSortedEntity? Pop()
    {
        TSortedEntity? item = ElementAtOrDefault(0);

        if (item is null)
        {
            return null;
        }

        Remove(item);
        return item;
    }

    public TSortedEntity? PopToBottom()
    {
        TSortedEntity? item = ElementAtOrDefault(Count - 1);

        if (item is null)
        {
            return null;
        }

        Remove(item);
        return item;
    }

    public TSortedEntity? GetByIdOrDefault(Guid id)
    {
        return _entities.GetByIdOrDefault(id);
    }

    public TSortedEntity GetById(Guid id)
    {
        return _entities.GetById(id);
    }
}
