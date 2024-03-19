namespace CCS.Domain.Exceptions;

public class WrongOperationException : BusinessException
{
    public override string ExceptionHash => "0000-0000-0004";

    public WrongOperationException() : base()
    {
    }

    public WrongOperationException(string message) : base(message)
    {
    }

    public WrongOperationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
