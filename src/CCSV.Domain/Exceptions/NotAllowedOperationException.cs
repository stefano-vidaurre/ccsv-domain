namespace CCSV.Domain.Exceptions;

public class NotAllowedOperationException : BusinessException
{
    public override string ExceptionHash => "0000-0000-0005";

    public NotAllowedOperationException() : base()
    {
    }

    public NotAllowedOperationException(string message) : base(message)
    {
    }

    public NotAllowedOperationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
