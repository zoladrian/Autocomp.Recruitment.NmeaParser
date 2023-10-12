using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;
using Autocomp.Nmea.Parsers.Interfaces;
using static Autocomp.Nmea.Models.NmeaEnums.GLLEnums;
using System.Globalization;

public class GLLParser : INmeaParser<GLLMessageData>
{
    public bool CanParse(string header)
    {
        return header == "GLL";
    }

    public GLLMessageData Parse(NmeaMessage message)
    {
        var fields = message.Fields;
        var latitude = ParseLatitude(fields[0]);
        var latitudeDirection = ParseLatitudeDirection(fields[1]);
        var longitude = ParseLongitude(fields[2]);
        var longitudeDirection = ParseLongitudeDirection(fields[3]);
        var utcTime = ParseUTCTime(fields[4]);
        var status = ParseStatus(fields[5]);
        var modeIndicator = ParseModeIndicator(fields[6]);

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

    private double ParseLatitude(string field)
    {
        return double.Parse(field, CultureInfo.InvariantCulture);
    }

    private LatitudeDirection ParseLatitudeDirection(string field)
    {
        return (LatitudeDirection)Enum.Parse(typeof(LatitudeDirection), field);
    }

    private double ParseLongitude(string field)
    {
        return double.Parse(field, CultureInfo.InvariantCulture);
    }

    private LongitudeDirection ParseLongitudeDirection(string field)
    {
        return (LongitudeDirection)Enum.Parse(typeof(LongitudeDirection), field);
    }

    private DateTime ParseUTCTime(string field)
    {
        return DateTime.ParseExact(field.Trim(), "HHmmss.ff", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
    }

    private Status ParseStatus(string field)
    {
        return (Status)Enum.Parse(typeof(Status), field);
    }

    private ModeIndicator ParseModeIndicator(string field)
    {
        return (ModeIndicator)Enum.Parse(typeof(ModeIndicator), field.Trim()[0].ToString());
    }
}
