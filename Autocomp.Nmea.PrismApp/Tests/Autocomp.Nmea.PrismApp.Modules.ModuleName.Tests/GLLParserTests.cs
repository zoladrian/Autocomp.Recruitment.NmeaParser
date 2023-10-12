using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;
using Autocomp.Nmea.Parsers.FieldParsers;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Collections.Generic;
using Xunit;
using static Autocomp.Nmea.Models.NmeaEnums.GLLEnums;

namespace Autocomp.Nmea.Parsers.Tests
{
    public class GLLParserTests
    {
        private readonly Mock<IValidator<GLLMessageData>> mockValidator;

        public GLLParserTests()
        {
            mockValidator = new Mock<IValidator<GLLMessageData>>();
        }

        [Fact]
        public void CanParse_ValidGLLHeader_ReturnsTrue()
        {
            var parser = new GLLParser(
                mockValidator.Object,
                new DoubleFieldParser(),
                new DateTimeFieldParser(),
                new EnumFieldParser<LatitudeDirection>(),
                new EnumFieldParser<LongitudeDirection>(),
                new EnumFieldParser<Status>(),
                new EnumFieldParser<ModeIndicator>()
                );

            bool result = parser.CanParse("GLL");

            Assert.True(result);
        }

        [Fact]
        public void Parse_ValidGLLMessage_ReturnsCorrectData()
        {
            mockValidator.Setup(x => x.Validate(It.IsAny<GLLMessageData>())).Returns(new FluentValidation.Results.ValidationResult());

            var parser = new GLLParser(
                mockValidator.Object,
                new DoubleFieldParser(),
                new DateTimeFieldParser(),
                new EnumFieldParser<LatitudeDirection>(),
                new EnumFieldParser<LongitudeDirection>(),
                new EnumFieldParser<Status>(),
                new EnumFieldParser<ModeIndicator>()
                );

            var message = new NmeaMessage("$GPGLL, 3953.88008971, N, 10506.75318910, W, 034138.00, A, D * 7A");

            var result = parser.Parse(message);

            Assert.True(result.Success);
            Assert.Equal(3953.88008971, result.Data.Latitude);
            Assert.Equal(LatitudeDirection.N, result.Data.LatitudeDirection);
            Assert.Equal(10506.75318910, result.Data.Longitude);
            Assert.Equal(LongitudeDirection.W, result.Data.LongitudeDirection);
            Assert.Equal(Status.A, result.Data.Status);
            Assert.Equal(ModeIndicator.D, result.Data.ModeIndicator);
        }

        [Fact]
        public void Parse_ValidatorFails_ReturnsError()
        {
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("SomeProperty", "Some error")
            };

            mockValidator.Setup(x => x.Validate(It.IsAny<GLLMessageData>())).Returns(new FluentValidation.Results.ValidationResult(validationFailures));

            var parser = new GLLParser(
                mockValidator.Object,
                new DoubleFieldParser(),
                new DateTimeFieldParser(),
                new EnumFieldParser<LatitudeDirection>(),
                new EnumFieldParser<LongitudeDirection>(),
                new EnumFieldParser<Status>(),
                new EnumFieldParser<ModeIndicator>()
                );

            var message = new NmeaMessage("$GPGLL, 3953.88008971, N, 10506.75318910, W, 034138.00, A, D * 7A");

            var result = parser.Parse(message);

            Assert.False(result.Success);
            Assert.Equal("Some error", result.ErrorMessage);
        }
    }
}