using Autocomp.Nmea.Parsers.Interfaces;
using System.Globalization;

namespace Autocomp.Nmea.Parsers.FieldParsers
{
    public class DateTimeFieldParser : IFieldParser<DateTime>
    {
        public bool TryParse(string field, out DateTime value)
        {
            string trimmedField = field.Trim();
            string[] formats = new[]
            {
                "HHmmss",
                "HHmmss.f",
                "HHmmss.ff",
                "HHmmss.fff",
            };

            foreach (var format in formats)
            {
                if (DateTime.TryParseExact(trimmedField, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out value))
                {
                    return true;
                }
            }

            value = default;
            return false;
        }
    }
}