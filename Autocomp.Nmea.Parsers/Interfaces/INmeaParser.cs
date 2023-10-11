using Autocomp.Nmea.Common;

namespace Autocomp.Nmea.Parsers.Interfaces
{
    public interface INmeaParser
    {
        object Parse(NmeaMessage message);
    }
}