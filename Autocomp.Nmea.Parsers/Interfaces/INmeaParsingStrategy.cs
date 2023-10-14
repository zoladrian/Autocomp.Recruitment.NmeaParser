using Autocomp.Nmea.Common;

namespace Autocomp.Nmea.Parsers.Interfaces
{
    public interface INmeaParsingStrategy
    {
        void Parse(NmeaMessage message, Dictionary<string, object> parsers, ref string parsedData, out string errorMessage);
    }
}