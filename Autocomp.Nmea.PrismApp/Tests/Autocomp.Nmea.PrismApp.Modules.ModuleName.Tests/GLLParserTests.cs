using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;
using Autocomp.Nmea.Parsers.FieldParsers;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using static Autocomp.Nmea.Models.NmeaEnums.GLLEnums;

namespace Autocomp.Nmea.Parsers.Tests
{
    public class GLLParserTests
    {
        private readonly Mock<IValidator<GLLMessageData>> mockValidator;
        private const double Tolerance = 0.000001;
        public GLLParserTests()
        {
            mockValidator = new Mock<IValidator<GLLMessageData>>();
        }

        [Fact]
        public void CanParse_ValidGLLHeader_ReturnsTrue()
        {
            var parser = CreateGLLParser();

            bool result = parser.CanParse("$GPGLL");

            Assert.True(result);
        }

        [Fact]
        public void Parse_ValidGLLMessage_ReturnsCorrectData()
        {
            mockValidator.Setup(x => x.Validate(It.IsAny<GLLMessageData>())).Returns(new ValidationResult());

            var parser = CreateGLLParser();

            var message = new NmeaMessage("$GPGLL, 3953.88008971, N, 10506.75318910, W, 034138.00, A, D * 7A");

            var result = parser.Parse(message);

            Assert.True(result.Success);
            AssertWithinTolerance(39.89800149516667, result.Data.Latitude);
            Assert.Equal(LatitudeDirection.N, result.Data.LatitudeDirection);
            AssertWithinTolerance(105.11255315166667, result.Data.Longitude);
            Assert.Equal(LongitudeDirection.W, result.Data.LongitudeDirection);

            //UTCTime check
            Assert.Equal(3, result.Data.UTCTime.Hour);
            Assert.Equal(41, result.Data.UTCTime.Minute);
            Assert.Equal(38, result.Data.UTCTime.Second);

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

            var parser = CreateGLLParser();

            var message = new NmeaMessage("$GPGLL, 3953.88008971, N, 10506.75318910, W, 034138.00, A, D * 7A");

            var result = parser.Parse(message);

            Assert.False(result.Success);
            Assert.Equal("Some error", result.ErrorMessage);
        }

        [Fact]
        public void CanParse_InvalidGLLHeader_ReturnsFalse()
        {
            var parser = CreateGLLParser();

            bool result = parser.CanParse("INVALID");

            Assert.False(result);
        }

        [Fact]
        public void Parse_InvalidLatitudeField_ReturnsError()
        {
            var parser = CreateGLLParser();
            var message = new NmeaMessage("$GPGLL, INVALID, N, 10506.75318910, W, 034138.00, A, D * 7A");

            var result = parser.Parse(message);

            Assert.False(result.Success);
            Assert.Equal("Invalid latitude value", result.ErrorMessage);
        }

        [Fact]
        public void Parse_InvalidLatitudeDirectionField_ReturnsError()
        {
            var parser = CreateGLLParser();
            var message = new NmeaMessage("$GPGLL, 3953.88008971, INVALID, 10506.75318910, W, 034138.00, A, D * 7A");

            var result = parser.Parse(message);

            Assert.False(result.Success);
            Assert.Equal("Invalid latitude direction", result.ErrorMessage);
        }

        [Fact]
        public void Parse_InvalidLongitudeField_ReturnsError()
        {
            var parser = CreateGLLParser();
            var message = new NmeaMessage("$GPGLL, 3953.88008971, N, INVALID, W, 034138.00, A, D * 7A");
            var result = parser.Parse(message);
            Assert.False(result.Success);
            Assert.Equal("Invalid longitude value", result.ErrorMessage);
        }

        [Fact]
        public void Parse_InvalidLongitudeDirectionField_ReturnsError()
        {
            var parser = CreateGLLParser();
            var message = new NmeaMessage("$GPGLL, 3953.88008971, N, 10506.75318910, INVALID, 034138.00, A, D * 7A");
            var result = parser.Parse(message);
            Assert.False(result.Success);
            Assert.Equal("Invalid longitude direction", result.ErrorMessage);
        }

        [Fact]
        public void Parse_InvalidUTCField_ReturnsError()
        {
            var parser = CreateGLLParser();
            var message = new NmeaMessage("$GPGLL, 3953.88008971, N, 10506.75318910, W, INVALID, A, D * 7A");
            var result = parser.Parse(message);
            Assert.False(result.Success);
            Assert.Equal("Invalid UTC time value", result.ErrorMessage);
        }

        [Fact]
        public void Parse_InvalidStatusField_ReturnsError()
        {
            var parser = CreateGLLParser();
            var message = new NmeaMessage("$GPGLL, 3953.88008971, N, 10506.75318910, W, 034138.00, INVALID, D * 7A");
            var result = parser.Parse(message);
            Assert.False(result.Success);
            Assert.Equal("Invalid status", result.ErrorMessage);
        }

        [Fact]
        public void Parse_InvalidModeIndicatorField_ReturnsError()
        {
            var parser = CreateGLLParser();
            var message = new NmeaMessage("$GPGLL, 3953.88008971, N, 10506.75318910, W, 034138.00, A, INVALID * 7A");
            var result = parser.Parse(message);
            Assert.False(result.Success);
            Assert.Equal("Invalid mode indicator", result.ErrorMessage);
        }
        /// <summary>
        /// Sprawdza czy dana wartość mieści się w tolerancji błędu
        /// </summary>
        private void AssertWithinTolerance(double expected, double actual)
        {
            Assert.True(Math.Abs(expected - actual) < Tolerance, $"Expected {expected} but got {actual}.");
        }
        private GLLParser CreateGLLParser()
        {
            return new GLLParser(
                mockValidator.Object,
                new DecimalFieldParser(),
                new DateTimeFieldParser(),
                new EnumFieldParser<LatitudeDirection>(),
                new EnumFieldParser<LongitudeDirection>(),
                new EnumFieldParser<Status>(),
                new EnumFieldParser<ModeIndicator>()
                );
        }
    }
}