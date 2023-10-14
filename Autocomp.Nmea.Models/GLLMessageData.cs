using Autocomp.Nmea.Models.NmeaEnums;
using static Autocomp.Nmea.Models.NmeaEnums.GLLEnums;

namespace Autocomp.Nmea.Models
{
    public class GLLMessageData
    {
        public double Latitude { get; set; }
        public LatitudeDirection LatitudeDirection { get; set; }
        public double Longitude { get; set; }
        public LongitudeDirection LongitudeDirection { get; set; }
        public DateTime UTCTime { get; set; }
        public Status Status { get; set; }
        public ModeIndicator ModeIndicator { get; set; }


        public GLLMessageData(
            double latitude,
            LatitudeDirection latitudeDirection,
            double longitude,
            LongitudeDirection longitudeDirection,
            DateTime utcTime, Status status,
            ModeIndicator modeIndicator
            )
        {
            Latitude = latitude;
            LatitudeDirection = latitudeDirection;
            Longitude = longitude;
            LongitudeDirection = longitudeDirection;
            UTCTime = utcTime;
            Status = status;
            ModeIndicator = modeIndicator;
        }
        public override string ToString()
        {
            string formattedLatitude = FormatCoordinate(Latitude, LatitudeDirection);
            string formattedLongitude = FormatCoordinate(Longitude, LongitudeDirection);
            string formattedTime = UTCTime.ToString("HH:mm:ss.fff") + " UTC";

            return $"Latitude: {formattedLatitude}\n" +
                   $"Longitude: {formattedLongitude}\n" +
                   $"UTC Time: {formattedTime}\n" +
                   $"Status: {Status.ToDescription()}\n" +
                   $"Mode Indicator: {ModeIndicator.ToDescription()}";
        }

        private string FormatCoordinate(double coordinate, Enum direction)
        {
            int degrees = (int)coordinate;
            double minutes = (coordinate - degrees) * 60;
            return $"{degrees}°{minutes:0.00}' {direction}";
        }
    }
}