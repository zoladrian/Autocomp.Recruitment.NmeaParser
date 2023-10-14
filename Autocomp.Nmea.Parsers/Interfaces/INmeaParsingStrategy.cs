using Autocomp.Nmea.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Parsers.Interfaces
{
    public interface INmeaParsingStrategy
    {
        void Parse(NmeaMessage message, Dictionary<string, object> parsers, ref string parsedData);
    }

}
