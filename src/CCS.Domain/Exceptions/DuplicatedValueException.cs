namespace CCS.Domain.Exceptions;

public class DuplicatedValueException : BusinessException
{
    public override string ExceptionHash => "0000-0000-0001";

    public DuplicatedValueException() : base()
    {
    }

    public DuplicatedValueException(string message) : base(message)
    {
    }

    public DuplicatedValueException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
