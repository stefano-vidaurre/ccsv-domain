namespace CCS.Domain.Entities.Collections;

public interface ISortedEntityList<TSortedEntity> : ICollection<TSortedEntity> where TSortedEntity : SortedEntity
{
    new void Add(TSortedEntity item);
    int IndexOf(TSortedEntity item);
    void Swap(TSortedEntity origin, TSortedEntity destiny);
    void Insert(TSortedEntity item, int index);
    void Move(TSortedEntity item, int index);
    void Push(TSortedEntity item);
    TSortedEntity? Pop();
    TSortedEntity? PopToBottom();
    TSortedEntity this[int index] { get; }
    TSortedEntity? GetByIdOrDefault(Guid id);
    TSortedEntity GetById(Guid id);
    TSortedEntity this[Guid id] { get; }
    TSortedEntity ElementAt(int index);
    TSortedEntity? ElementAtOrDefault(int index);
}
