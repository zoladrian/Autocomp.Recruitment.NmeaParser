using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;
using Autocomp.Nmea.Parsers.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;
using static Autocomp.Nmea.Models.NmeaEnums.GLLEnums;

namespace Autocomp.Nmea.Parsers.Tests
{
    public class ReflectionBasedNmeaParsingStrategyTests
    {
        [Fact]
        public void Parse_WithValidParserAndMessage_ReturnsCorrectData()
        {
            var mockParser = new Mock<INmeaParser<GLLMessageData>>();
            mockParser.Setup(p => p.Parse(It.IsAny<NmeaMessage>()))
                .Returns(new ParseResult<GLLMessageData>
                {
                    Success = true,
                    Data = new GLLMessageData(
                        49.274167,
                        LatitudeDirection.N,
                        123.185333,
                        LongitudeDirection.W,
                        DateTime.ParseExact("225444.00", "HHmmss.ff", CultureInfo.InvariantCulture),
                        Status.A,
                        ModeIndicator.A
                    )
                });

            var message = new NmeaMessage("$GPGLL,4916.45,N,12311.12,W,225444.00,A,A*61");
            string errorMessage;
            var parsers = new Dictionary<string, object> { { "GLL", mockParser.Object } };
            var sut = new ReflectionBasedNmeaParsingStrategy();
            string parsedData = null;

            sut.Parse(message, parsers, ref parsedData, out errorMessage);
            Assert.Null(errorMessage);
            Assert.NotNull(parsedData);
        }

        [Fact]
        public void Parse_WithInvalidHeader_ReturnsUnknownMessage()
        {
            var message = new NmeaMessage("INVALID,4916.45,N,12311.12,W,225444.00,A,A*61");
            var parsers = new Dictionary<string, object>();
            string errorMessage;
            var sut = new ReflectionBasedNmeaParsingStrategy();
            string parsedData = null;

            sut.Parse(message, parsers, ref parsedData, out errorMessage);

            Assert.Equal("Unknown message", errorMessage);
        }
    }
}