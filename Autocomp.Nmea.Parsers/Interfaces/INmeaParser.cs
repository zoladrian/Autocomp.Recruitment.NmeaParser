using Autocomp.Nmea.Common;

namespace Autocomp.Nmea.Parsers.Interfaces
{
    public interface INmeaParser
    {
        bool CanParse(string header);
        object Parse(NmeaMessage message);
    }
}