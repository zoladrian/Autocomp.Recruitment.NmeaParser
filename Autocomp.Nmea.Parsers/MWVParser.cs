using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;
using Autocomp.Nmea.Parsers.Interfaces;
using System.Globalization;
using static Autocomp.Nmea.Models.NmeaEnums.MWVEnums;

namespace Autocomp.Nmea.Parsers
{
    public class MWVParser : INmeaParser<MWVMessageData>
    {
        public bool CanParse(string header)
        {
            return header == "MWV";
        }

        public MWVMessageData Parse(NmeaMessage message)
        {
            var fields = message.Fields;
            var windAngle = ParseWindAngle(fields[0]);
            var reference = ParseReference(fields[1]);
            var windSpeed = ParseWindSpeed(fields[2]);
            var windSpeedUnits = ParseWindSpeedUnits(fields[3]);
            var status = ParseStatus(fields[4]);

            return new MWVMessageData(
                windAngle,
                reference,
                windSpeed,
                windSpeedUnits,
                status
            );
        }

        private double ParseWindAngle(string field)
        {
            return double.Parse(field, CultureInfo.InvariantCulture);
        }

        private WindSpeedUnits ParseWindSpeedUnits(string field)
        {
            return (WindSpeedUnits)Enum.Parse(typeof(WindSpeedUnits), field);
        }

        private double ParseWindSpeed(string field)
        {
            return double.Parse(field, CultureInfo.InvariantCulture);
        }

        private Reference ParseReference(string field)
        {
            return (Reference)Enum.Parse(typeof(Reference), field);
        }

        private Status ParseStatus(string field)
        {
            return (Status)Enum.Parse(typeof(Status), field.Trim()[0].ToString());
        }
    }

}
