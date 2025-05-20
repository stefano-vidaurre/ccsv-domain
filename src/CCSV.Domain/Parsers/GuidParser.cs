namespace CCSV.Domain.Parsers;

public static class GuidParser
{
    const int GuidLength = 16;
    const int DateTimeLength = 8;

    public static Guid Parse(DateTime value)
    {
        byte[] guidBytes = new byte[GuidLength];

        byte[] dateBytes = BitConverter.GetBytes(value.Ticks);
        byte kindByte = (byte)value.Kind;

        Array.Copy(dateBytes, 0, guidBytes, 0, DateTimeLength);
        guidBytes[DateTimeLength] = kindByte;

        return new Guid(guidBytes);
    }
}
