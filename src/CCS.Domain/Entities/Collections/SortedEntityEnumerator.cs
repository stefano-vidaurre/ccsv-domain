using System.Collections;

namespace CCS.Domain.Entities.Collections;

public class SortedEntityEnumerator<TSortedEntity> : IEnumerator<TSortedEntity> where TSortedEntity : SortedEntity
{
    private readonly IList<TSortedEntity> _items;
    private TSortedEntity? _current;
    private int _count;

    public SortedEntityEnumerator(IEnumerable<TSortedEntity> items)
    {
        _items = new List<TSortedEntity>(items);
        _current = default;
        _count = -1;
    }

#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
    public TSortedEntity? Current => _current;
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).

    object? IEnumerator.Current => Current;

    public bool MoveNext()
    {
        if (!HasNext())
        {
            return false;
        }

        _count++;
        _current = _items[_count];
        return true;
    }

    private bool HasNext()
    {
        return _count + 1 < _items.Count;
    }

    public void Reset()
    {
        _current = default;
        _count = -1;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        // Cleanup
    }
}
