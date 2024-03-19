namespace CCSV.Domain.Repositories.Exceptions;

public class ArgumentEntityException : InternalRepositoryException
{
    public override string ExceptionHash => "0000-0000-1001";

    public ArgumentEntityException() : base()
    {
    }

    public ArgumentEntityException(string message) : base(message)
    {
    }

    public ArgumentEntityException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
