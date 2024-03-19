namespace CCSV.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public Guid EntityConcurrencyToken { get; private set; }
    public DateTime EntityCreationDate { get; private set; }
    public DateTime EntityEditionDate { get; private set; }

    protected Entity(Guid id)
    {
        Id = id;
        EntityCreationDate = DateTime.Now;
        EntityEditionDate = EntityCreationDate;
        EntityConcurrencyToken = Guid.NewGuid();
    }

    public override bool Equals(object? obj)
    {
        Entity? compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo))
        {
            return true;
        }

        if (compareTo is null)
        {
            return false;
        }

        return Id.Equals(compareTo.Id);
    }

#pragma warning disable S3875 // "operator==" should not be overloaded on reference types
    public static bool operator ==(Entity a, Entity b)
#pragma warning restore S3875 // "operator==" should not be overloaded on reference types
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return GetType().GetHashCode() * 17 + Id.GetHashCode() * 17 + EntityConcurrencyToken.GetHashCode();
    }

    public override string ToString()
    {
        return GetType().Name + " [Id = " + Id + "]";
    }

    public void SetAsEdited()
    {
        EntityEditionDate = DateTime.Now;
        EntityConcurrencyToken = Guid.NewGuid();
    }
}
