namespace CCSV.Domain.Exceptions;

public class InvalidValueException : BusinessException
{
    public override string ExceptionHash => "0000-0000-0002";

    public InvalidValueException() : base()
    {
    }

    public InvalidValueException(string message) : base(message)
    {
    }

    public InvalidValueException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
