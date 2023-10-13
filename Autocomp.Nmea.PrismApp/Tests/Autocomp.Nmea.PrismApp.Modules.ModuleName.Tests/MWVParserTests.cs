using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;
using Autocomp.Nmea.Parsers.FieldParsers;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Collections.Generic;
using Xunit;
using static Autocomp.Nmea.Models.NmeaEnums.MWVEnums;

namespace Autocomp.Nmea.Parsers.Tests
{
    public class MWVParserTests
    {
        private readonly Mock<IValidator<MWVMessageData>> mockValidator;

        public MWVParserTests()
        {
            mockValidator = new Mock<IValidator<MWVMessageData>>();
        }

        [Fact]
        public void CanParse_ValidMWVHeader_ReturnsTrue()
        {
            var parser = CreateMWVParser();

            bool result = parser.CanParse("MWV");

            Assert.True(result);
        }

        [Fact]
        public void Parse_ValidMWVMessage_ReturnsCorrectData()
        {
            mockValidator.Setup(x => x.Validate(It.IsAny<MWVMessageData>())).Returns(new ValidationResult());

            var parser = CreateMWVParser();

            var message = new NmeaMessage("$WIMWV, 320, R, 15.0, M, A * 0B");

            var result = parser.Parse(message);

            Assert.True(result.Success);
            Assert.Equal(320.0, result.Data.WindAngle);
            Assert.Equal(Reference.R, result.Data.Reference);
            Assert.Equal(15.0, result.Data.WindSpeed);
            Assert.Equal(WindSpeedUnits.M, result.Data.WindSpeedUnits);
            Assert.Equal(Status.A, result.Data.Status);
        }

        [Fact]
        public void Parse_ValidatorFails_ReturnsError()
        {
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("SomeProperty", "Some error")
            };

            mockValidator.Setup(x => x.Validate(It.IsAny<MWVMessageData>())).Returns(new ValidationResult(validationFailures));

            var parser = CreateMWVParser();

            var message = new NmeaMessage("$WIMWV, 320, R, 15.0, M, A * 0B");

            var result = parser.Parse(message);

            Assert.False(result.Success);
            Assert.Equal("Some error", result.ErrorMessage);
        }

        [Fact]
        public void Parse_InvalidWindAngleField_ReturnsError()
        {
            var parser = CreateMWVParser();
            var message = new NmeaMessage("$WIMWV, INVALID, R, 15.0, M, A * 0B");
            var result = parser.Parse(message);
            Assert.False(result.Success);
            Assert.Equal("Invalid wind angle", result.ErrorMessage);
        }

        [Fact]
        public void Parse_InvalidReferenceField_ReturnsError()
        {
            var parser = CreateMWVParser();
            var message = new NmeaMessage("$WIMWV, 320, INVALID, 15.0, M, A * 0B");
            var result = parser.Parse(message);
            Assert.False(result.Success);
            Assert.Equal("Invalid reference", result.ErrorMessage);
        }

        [Fact]
        public void Parse_InvalidWindSpeedField_ReturnsError()
        {
            var parser = CreateMWVParser();
            var message = new NmeaMessage("$WIMWV, 320, R, INVALID, M, A * 0B");
            var result = parser.Parse(message);
            Assert.False(result.Success);
            Assert.Equal("Invalid wind speed", result.ErrorMessage);
        }

        [Fact]
        public void Parse_InvalidWindSpeedUnitsField_ReturnsError()
        {
            var parser = CreateMWVParser();
            var message = new NmeaMessage("$WIMWV, 320, R, 15.0, INVALID, A * 0B");
            var result = parser.Parse(message);
            Assert.False(result.Success);
            Assert.Equal("Invalid wind speed units", result.ErrorMessage);
        }

        [Fact]
        public void Parse_InvalidStatusField_ReturnsError()
        {
            var parser = CreateMWVParser();
            var message = new NmeaMessage("$WIMWV, 320, R, 15.0, M, INVALID * 0B");
            var result = parser.Parse(message);
            Assert.False(result.Success);
            Assert.Equal("Invalid status", result.ErrorMessage);
        }

        private MWVParser CreateMWVParser()
        {
            return new MWVParser(
                mockValidator.Object,
                new DoubleFieldParser(),
                new EnumFieldParser<WindSpeedUnits>(),
                new EnumFieldParser<Reference>(),
                new EnumFieldParser<Status>()
            );
        }
    }
}