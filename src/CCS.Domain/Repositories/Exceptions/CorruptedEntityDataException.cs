namespace CCS.Domain.Repositories.Exceptions;

public class CorruptedEntityDataException : InternalRepositoryException
{
    public override string ExceptionHash => "0000-0000-1003";

    public CorruptedEntityDataException() : base()
    {
    }

    public CorruptedEntityDataException(string message) : base(message)
    {
    }

    public CorruptedEntityDataException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
