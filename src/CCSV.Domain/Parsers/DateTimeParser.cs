using System.Globalization;
using CCSV.Domain.Exceptions;

namespace CCSV.Domain.Parsers;

public static class DateTimeParser
{
    public static DateTime ParseUTC(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidValueException($"DateTime UTC value cant be null/empty.");
        }

        if (!TryParseUTC(value, out DateTime result))
        {
            throw new InvalidValueException($"DateTime UTC value ({value}) is not valid.");
        }

        return result;
    }

    public static bool TryParseUTC(string? value, out DateTime parsed)
    {
        if (value is null)
        {
            parsed = default;
            return false;
        }

        if (!DateTime.TryParse(value, CultureInfo.InvariantCulture, out parsed))
        {
            parsed = default;
            return false;
        }

        parsed = parsed.ToUniversalTime();
        return true;
    }
}
