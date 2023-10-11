using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;
using Autocomp.Nmea.Parsers.Interfaces;
using System.Globalization;
using static Autocomp.Nmea.Models.NmeaEnums.GLLEnums;

namespace Autocomp.Nmea.Parsers
{
    public class GLLParser : INmeaParser
    {
        public bool CanParse(string header)
        {
            return header == "GLL";
        }

        public object Parse(NmeaMessage message)
        {
            var fields = message.Fields;

            double latitude = double.Parse(fields[0], CultureInfo.InvariantCulture);
            LatitudeDirection latitudeDirection = (LatitudeDirection)Enum.Parse(typeof(LatitudeDirection), fields[1]);
            double longitude = double.Parse(fields[2], CultureInfo.InvariantCulture);
            LongitudeDirection longitudeDirection = (LongitudeDirection)Enum.Parse(typeof(LongitudeDirection), fields[3]);

            // póki co zakładam że czas ma format HHmmss
            DateTime utcTime = DateTime.ParseExact("125947", "HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
            Status status = (Status)Enum.Parse(typeof(Status), fields[5]);
            ModeIndicator modeIndicator = (ModeIndicator)Enum.Parse(typeof(ModeIndicator), fields[6]);

            return new GLLMessageData(
                latitude,
                latitudeDirection,
                longitude,
                longitudeDirection,
                utcTime,
                status,
                modeIndicator
            );
        }
    }
}