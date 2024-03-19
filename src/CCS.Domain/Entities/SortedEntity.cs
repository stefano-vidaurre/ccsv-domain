namespace CCS.Domain.Entities;

public abstract class SortedEntity : Entity
{
    public long SortedEntityWeight { get; private set; }

    protected SortedEntity(Guid id) : base(id)
    {
        SortedEntityWeight = 0;
    }

    internal void SetWeight(long weight)
    {
        SortedEntityWeight = weight;
    }
}
