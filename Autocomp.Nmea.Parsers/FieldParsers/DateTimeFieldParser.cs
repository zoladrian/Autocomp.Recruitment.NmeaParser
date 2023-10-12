using Autocomp.Nmea.Parsers.Interfaces;
using System.Globalization;

namespace Autocomp.Nmea.Parsers.FieldParsers
{
    public class DateTimeFieldParser : IFieldParser<DateTime>
    {
        public bool TryParse(string field, out DateTime value)
        {
            return DateTime.TryParseExact(field.Trim(), "HHmmss.ff", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out value);
        }
    }
}