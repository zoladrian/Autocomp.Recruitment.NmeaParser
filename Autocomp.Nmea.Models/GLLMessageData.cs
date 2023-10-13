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
            return $"Latitude: {Latitude}\n" +
                   $"LatitudeDirection: {LatitudeDirection}\n" +
                   $"Longitude: {Longitude}\n" +
                   $"LongitudeDirection: {LongitudeDirection}\n" +
                   $"UTCTime: {UTCTime}\n" +
                   $"Status: {Status}\n" +
                   $"ModeIndicator: {ModeIndicator}";
        }
    }
}