namespace CCSV.Domain.Repositories.Exceptions;

public class RepositoryOperationException : InternalRepositoryException
{
    public override string ExceptionHash => "0000-0000-1002";

    public RepositoryOperationException() : base()
    {
    }

    public RepositoryOperationException(string message) : base(message)
    {
    }

    public RepositoryOperationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
