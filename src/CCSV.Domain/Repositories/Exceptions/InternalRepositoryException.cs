using CCSV.Domain.Exceptions;

namespace CCSV.Domain.Repositories.Exceptions;

public class InternalRepositoryException : DomainException
{
    public override string ExceptionHash => "0000-0000-1000";

    public InternalRepositoryException() : base()
    {
    }

    public InternalRepositoryException(string message) : base(message)
    {
    }

    public InternalRepositoryException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
