using Autocomp.Nmea.Parsers.Interfaces;
using System.Globalization;

namespace Autocomp.Nmea.Parsers.FieldParsers
{
    public class DoubleFieldParser : IFieldParser<double>
    {
        public bool TryParse(string field, out double value)
        {
            return double.TryParse(field, NumberStyles.Any, CultureInfo.InvariantCulture, out value);
        }
    }
}