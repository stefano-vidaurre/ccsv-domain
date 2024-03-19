namespace CCS.Domain.Exceptions;

public class BadGatewayException : BusinessException
{
    public override string ExceptionHash => "0000-0000-0006";

    public BadGatewayException() : base()
    {
    }

    public BadGatewayException(string message) : base(message)
    {
    }

    public BadGatewayException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
