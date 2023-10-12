using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;

namespace Autocomp.Nmea.Parsers.Interfaces
{
    public interface INmeaParser<T>
    {
        bool CanParse(string header);
        ParseResult<T> Parse(NmeaMessage message);
    }
}