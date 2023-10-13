using Autocomp.Nmea.Models.NmeaEnums;
using static Autocomp.Nmea.Models.NmeaEnums.MWVEnums;

namespace Autocomp.Nmea.Models
{
    public class MWVMessageData
    {
        public double WindAngle { get; set; }
        public Reference Reference { get; set; }
        public double WindSpeed { get; set; }
        public WindSpeedUnits WindSpeedUnits { get; set; }
        public Status Status { get; set; }

        public MWVMessageData(
            double windAngle,
            Reference reference,
            double windSpeed,
            WindSpeedUnits windSpeedUnits,
            Status status
        )
        {
            WindAngle = windAngle;
            WindSpeedUnits = windSpeedUnits;
            WindSpeed = windSpeed;
            Reference = reference;
            Status = status;
        }
        public override string ToString()
        {
            return $"WindAngle: {WindAngle}\n" +
                   $"Reference: {Reference.ToDescription()}\n" +
                   $"WindSpeed: {WindSpeed} { WindSpeedUnits.ToDescription()}\n" +
                   $"Status: {Status.ToDescription()}";
        }

    }
}