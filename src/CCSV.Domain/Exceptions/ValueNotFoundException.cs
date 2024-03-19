namespace CCSV.Domain.Exceptions;

public class ValueNotFoundException : BusinessException
{
    public override string ExceptionHash => "0000-0000-0003";

    public ValueNotFoundException() : base()
    {
    }

    public ValueNotFoundException(string message) : base(message)
    {
    }

    public ValueNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
