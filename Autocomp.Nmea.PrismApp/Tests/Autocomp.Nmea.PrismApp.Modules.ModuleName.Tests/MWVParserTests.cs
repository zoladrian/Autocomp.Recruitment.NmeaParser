using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;
using Xunit;
using static Autocomp.Nmea.Models.NmeaEnums.MWVEnums;

namespace Autocomp.Nmea.Parsers.Tests
{
    public class MWVParserTests
    {
        [Fact]
        public void CanParse_ValidMWVHeader_ReturnsTrue()
        {
            var parser = new MWVParser();
            bool result = parser.CanParse("MWV");
            Assert.True(result);
        }

        [Fact]
        public void CanParse_InvalidHeader_ReturnsFalse()
        {
            var parser = new MWVParser();
            bool result = parser.CanParse("INVALID");
            Assert.False(result);
        }

        [Fact]
        public void Parse_ValidMWVMessage_ReturnsCorrectData()
        {
            var parser = new MWVParser();
            var message = new NmeaMessage("$WIMWV, 320, R, 15.0, M, A * 0B < CR >< LF >");

            MWVMessageData result = parser.Parse(message);

            Assert.Equal(320.0, result.WindAngle);
            Assert.Equal(Reference.R, result.Reference);
            Assert.Equal(15.0, result.WindSpeed);
            Assert.Equal(WindSpeedUnits.M, result.WindSpeedUnits);
            Assert.Equal(Status.A, result.Status);
        }
    }
}