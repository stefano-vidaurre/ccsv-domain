using CCSV.Domain.Exceptions;
using System.Globalization;

namespace CCSV.Domain.Parsers;

public static class DateTimeParser
{
    public static DateTime ParseUTC(string? value)
    {
        return Parse(value).ToUniversalTime();
    }
    public static DateTime ParseUTC(Guid value)
    {
        return Parse(value).ToUniversalTime();
    }

    public static bool TryParseUTC(string? value, out DateTime parsed)
    {
        if (!TryParse(value, out parsed))
        {
            return false;
        }

        parsed = parsed.ToUniversalTime();
        return true;
    }

    public static DateTime Parse(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidValueException($"DateTime value cant be null/empty.");
        }

        if (!TryParse(value, out DateTime result))
        {
            throw new InvalidValueException($"DateTime value ({value}) is not valid.");
        }

        return result;
    }

    public static DateTime Parse(Guid value)
    {
        byte[] bytes = value.ToByteArray();

        long ticks = BitConverter.ToInt64(bytes, 0);
        DateTimeKind kind = (DateTimeKind)bytes[8];

        return new DateTime(ticks, kind);
    }

    public static bool TryParse(string? value, out DateTime parsed)
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

        return true;
    }

    public static DateTime ParseUTCExact(string? value, string format)
    {
        return ParseExact(value, format).ToUniversalTime();
    }

    public static bool TryParseUTCExact(string? value, string format, out DateTime parsed)
    {
        if (!TryParseExact(value, format, out parsed))
        {
            return false;
        }

        parsed = parsed.ToUniversalTime();
        return true;
    }

    public static DateTime ParseExact(string? value, string format)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidValueException($"DateTime value cant be null/empty.");
        }

        if (!TryParseExact(value, format, out DateTime result))
        {
            throw new InvalidValueException($"DateTime value ({value}) is not valid.");
        }

        return result;
    }

    public static bool TryParseExact(string? value, string format, out DateTime parsed)
    {
        if (value is null)
        {
            parsed = default;
            return false;
        }

        if (!DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed))
        {
            parsed = default;
            return false;
        }

        return true;
    }
}
