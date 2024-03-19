namespace CCSV.Domain.Repositories.Exceptions;

public class EntityUpdateConcurrencyException : InternalRepositoryException
{
    public override string ExceptionHash => "0000-0000-1004";

    public EntityUpdateConcurrencyException() : base()
    {
    }

    public EntityUpdateConcurrencyException(string message) : base(message)
    {
    }

    public EntityUpdateConcurrencyException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
