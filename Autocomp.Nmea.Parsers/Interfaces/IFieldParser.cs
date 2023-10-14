namespace Autocomp.Nmea.Parsers.Interfaces
{
    public interface IFieldParser<T>
    {
        bool TryParse(string field, out T value);
    }
}