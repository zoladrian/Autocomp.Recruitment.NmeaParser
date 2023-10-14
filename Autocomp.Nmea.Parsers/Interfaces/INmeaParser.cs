using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;

namespace Autocomp.Nmea.Parsers.Interfaces
{
    public interface INmeaParser<T>
    {
        ParseResult<T> Parse(NmeaMessage message);
    }
}