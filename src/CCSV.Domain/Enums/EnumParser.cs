using CCSV.Domain.Exceptions;

namespace CCSV.Domain.Enums;

public static class EnumParser
{
    public static TEnum Parse<TEnum>(string? value) where TEnum : struct
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidValueException($"Enum value cant be null/empty.");
        }

        if (!TryParse(value, out TEnum parsed))
        {
            string names = PrintEnumNames<TEnum>();
            throw new InvalidValueException($"Enum value ({value}) is not valid. Only the following values are valid: {names}.");
        }

        return parsed;
    }

    public static bool TryParse<TEnum>(string? value, out TEnum parsed) where TEnum : struct
    {
        if (value is null)
        {
            parsed = default;
            return false;
        }

        return Enum.TryParse(value, out parsed);
    }

    private static string PrintEnumNames<TEnum>() where TEnum : struct
    {
        return string.Join(", ", Enum.GetNames(typeof(TEnum)));
    }
}
