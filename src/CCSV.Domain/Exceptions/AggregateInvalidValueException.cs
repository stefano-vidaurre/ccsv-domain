using System.Collections.ObjectModel;
using System.Text;

namespace CCSV.Domain.Exceptions;

public class AggregateInvalidValueException : InvalidValueException
{
    private readonly IEnumerable<InvalidValueException> _innerExceptions;
    private readonly string? _message;
    public override string ExceptionHash => "0000-0000-0005";

    public override string Message => _message ?? base.Message;
    public IEnumerable<InvalidValueException> InnerExceptions => _innerExceptions.AsEnumerable();

    public AggregateInvalidValueException() : base("One or more InvalidValueException occurred.")
    {
        _innerExceptions = Enumerable.Empty<InvalidValueException>();
    }

    public AggregateInvalidValueException(string message) : base(message)
    {
        _innerExceptions = Enumerable.Empty<InvalidValueException>();
    }

    public AggregateInvalidValueException(string message, Exception innerException) : base(message, innerException)
    {
        _innerExceptions = Enumerable.Empty<InvalidValueException>();
    }

    public AggregateInvalidValueException(params InvalidValueException[] innerExceptions) : this((IEnumerable<InvalidValueException>) innerExceptions)
    {
    }

    public AggregateInvalidValueException(IEnumerable<InvalidValueException> innerExceptions) : this("", innerExceptions)
    {
        var sb = new StringBuilder(base.ToString());
        int i = 0;
        foreach (var exception in InnerExceptions)
        {
            sb.AppendLine();
            sb.Append($"---> (Inner Exception #{i}) ");
            sb.AppendLine(exception.ToString());
            sb.Append("<---");
            i++;
        }
        _message = sb.ToString();
    }

    public AggregateInvalidValueException(string message, params InvalidValueException[] innerExceptions) : this(message, (IEnumerable<InvalidValueException>)innerExceptions)
    {
    }

    public AggregateInvalidValueException(string message, IEnumerable<InvalidValueException> innerExceptions) : base(message, innerExceptions.FirstOrDefault(new InvalidValueException("One or more InvalidValueException occurred.")))
    {
        if (innerExceptions.Any())
        {
            _innerExceptions = innerExceptions;
        }
        else
        {
            _innerExceptions = new[] { (InvalidValueException)InnerException! };
        }
    }
}
