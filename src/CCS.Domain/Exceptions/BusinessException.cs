namespace CCS.Domain.Exceptions;

public class BusinessException : DomainException
{
    public override string ExceptionHash => "0000-0000-0000";

    public BusinessException() : base()
    {
    }

    public BusinessException(string message) : base(message)
    {
    }

    public BusinessException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
