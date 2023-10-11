using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;
using Autocomp.Nmea.Parsers;
using System;
using System.Globalization;
using Xunit;
using static Autocomp.Nmea.Models.NmeaEnums.GLLEnums;

public class GLLParserTests
{
    [Fact]
    public void CanParse_ValidGLLHeader_ReturnsTrue()
    {
        var parser = new GLLParser();
        bool result = parser.CanParse("GLL");
        Assert.True(result);
    }

    [Fact]
    public void CanParse_InvalidHeader_ReturnsFalse()
    {
        var parser = new GLLParser();
        bool result = parser.CanParse("INVALID");
        Assert.False(result);
    }

    [Fact]
    public void Parse_ValidGLLMessage_ReturnsCorrectData()
    {
        var parser = new GLLParser();
        var message = new NmeaMessage("GLL", new string[] {
            "4812.11", "N", "12311.12", "W", "125947", "A", "A"
        });

        var result = (GLLMessageData)parser.Parse(message);

        Assert.Equal(4812.11, result.Latitude);
        Assert.Equal(LatitudeDirection.N, result.LatitudeDirection);
        Assert.Equal(12311.12, result.Longitude);
        Assert.Equal(LongitudeDirection.W, result.LongitudeDirection);
        Assert.Equal(DateTime.ParseExact("125947", "HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal), result.UTCTime);
        Assert.Equal(Status.A, result.Status);
        Assert.Equal(ModeIndicator.A, result.ModeIndicator);
    }
}
