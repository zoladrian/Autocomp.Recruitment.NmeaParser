using Autocomp.Nmea.Parsers.Interfaces;
using System.Globalization;

namespace Autocomp.Nmea.Parsers.FieldParsers
{
    public class DecimalFieldParser : IFieldParser<decimal>
    {
        public bool TryParse(string field, out decimal value)
        {
            return decimal.TryParse(field, NumberStyles.Any, CultureInfo.InvariantCulture, out value);
        }
    }
}