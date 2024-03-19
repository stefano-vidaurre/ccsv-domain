namespace CCS.Domain.Exceptions;

public abstract class DomainException : Exception
{
    public abstract string ExceptionHash { get; }

    protected DomainException() : base()
    {
    }

    protected DomainException(string message) : base(message)
    {
    }

    protected DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
