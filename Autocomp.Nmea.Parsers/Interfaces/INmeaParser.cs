using Autocomp.Nmea.Common;

namespace Autocomp.Nmea.Parsers.Interfaces
{
    public interface INmeaParser<T>
    {
        bool CanParse(string header);
        T Parse(NmeaMessage message);
    }
}