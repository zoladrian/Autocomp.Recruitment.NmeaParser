using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;
using Autocomp.Nmea.Parsers.Interfaces;
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
        INmeaParser<GLLMessageData> parser = new GLLParser();
        var message = new NmeaMessage("$GPGLL, 3953.88008971, N, 10506.75318910, W, 034138.00, A, D * 7A");

        GLLMessageData result = parser.Parse(message);

        Assert.Equal(3953.88008971, result.Latitude);
        Assert.Equal(LatitudeDirection.N, result.LatitudeDirection);
        Assert.Equal(10506.75318910, result.Longitude);
        Assert.Equal(LongitudeDirection.W, result.LongitudeDirection);
        Assert.Equal(DateTime.ParseExact("034138.00", "HHmmss.ff", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal), result.UTCTime);
        Assert.Equal(Status.A, result.Status);
        Assert.Equal(ModeIndicator.D, result.ModeIndicator);
    }
}