namespace CCSV.Domain.Entities.Collections;

public class EntityEnumerator<TEntity> : IEnumerator<TEntity> where TEntity : Entity
{
    private readonly IList<TEntity> _items;
    private TEntity? _current;
    private int _count;

    public EntityEnumerator(IEnumerable<TEntity> items)
    {
        _items = new List<TEntity>(items);
        _current = default;
        _count = -1;
    }

#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
    public TEntity? Current => _current;
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).

    object? System.Collections.IEnumerator.Current => Current;

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
