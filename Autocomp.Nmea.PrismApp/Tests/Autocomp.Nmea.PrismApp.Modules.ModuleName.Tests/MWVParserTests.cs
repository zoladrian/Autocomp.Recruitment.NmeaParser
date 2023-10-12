using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;
using Autocomp.Nmea.Parsers.FieldParsers;
using Autocomp.Nmea.Parsers.Interfaces;
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
            var parser = new MWVParser(
                mockValidator.Object,
                new DoubleFieldParser(),
                new EnumFieldParser<WindSpeedUnits>(),
                new EnumFieldParser<Reference>(),
                new EnumFieldParser<Status>()
            );

            bool result = parser.CanParse("MWV");

            Assert.True(result);
        }

        [Fact]
        public void Parse_ValidMWVMessage_ReturnsCorrectData()
        {
            // Arrange
            mockValidator.Setup(x => x.Validate(It.IsAny<MWVMessageData>())).Returns(new ValidationResult());

            var parser = new MWVParser(
                mockValidator.Object,
                new DoubleFieldParser(),
                new EnumFieldParser<WindSpeedUnits>(),
                new EnumFieldParser<Reference>(),
                new EnumFieldParser<Status>()
            );

            var message = new NmeaMessage("$WIMWV, 320, R, 15.0, M, A * 0B");

            // Act
            var result = parser.Parse(message);

            // Assert
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
            // Arrange
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("SomeProperty", "Some error")
            };

            mockValidator.Setup(x => x.Validate(It.IsAny<MWVMessageData>())).Returns(new ValidationResult(validationFailures));

            var parser = new MWVParser(
                mockValidator.Object,
                new DoubleFieldParser(),
                new EnumFieldParser<WindSpeedUnits>(),
                new EnumFieldParser<Reference>(),
                new EnumFieldParser<Status>()
            );

            var message = new NmeaMessage("$WIMWV, 320, R, 15.0, M, A * 0B");

            // Act
            var result = parser.Parse(message);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Some error", result.ErrorMessage);
        }
    }
}
