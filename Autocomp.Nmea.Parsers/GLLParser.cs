using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;
using Autocomp.Nmea.Parsers.Interfaces;
using FluentValidation;
using System.Text.RegularExpressions;
using static Autocomp.Nmea.Models.NmeaEnums.GLLEnums;

namespace Autocomp.Nmea.Parsers
{
    public class GLLParser : INmeaParser<GLLMessageData>
    {
        private readonly IValidator<GLLMessageData> validator;
        private readonly IFieldParser<double> doubleFieldParser;
        private readonly IFieldParser<DateTime> dateTimeFieldParser;
        private readonly IFieldParser<LatitudeDirection> latitudeDirectionParser;
        private readonly IFieldParser<LongitudeDirection> longitudeDirectionParser;
        private readonly IFieldParser<Status> statusParser;
        private readonly IFieldParser<ModeIndicator> modeIndicatorParser;

        public GLLParser(
            IValidator<GLLMessageData> validator,
            IFieldParser<double> doubleFieldParser,
            IFieldParser<DateTime> dateTimeFieldParser,
            IFieldParser<LatitudeDirection> latitudeDirectionParser,
            IFieldParser<LongitudeDirection> longitudeDirectionParser,
            IFieldParser<Status> statusParser,
            IFieldParser<ModeIndicator> modeIndicatorParser
            )
        {
            this.validator = validator;
            this.doubleFieldParser = doubleFieldParser;
            this.dateTimeFieldParser = dateTimeFieldParser;
            this.latitudeDirectionParser = latitudeDirectionParser;
            this.longitudeDirectionParser = longitudeDirectionParser;
            this.statusParser = statusParser;
            this.modeIndicatorParser = modeIndicatorParser;
        }
        /// <summary>
        /// Sprawdza czy wiadomość może być przetworzona przez ten parser.
        /// </summary>
        public bool CanParse(string header)
        {
            return Regex.IsMatch(header, @"^\$..GLL");
        }
        /// <summary>
        /// Przetwarza wiadomość NMEA na obiekt danych GLL.
        /// </summary>
        public ParseResult<GLLMessageData> Parse(NmeaMessage message)
        {
            var fields = message.Fields;

            if (!doubleFieldParser.TryParse(fields[0], out var latitude))
            {
                return new ParseResult<GLLMessageData> { Success = false, ErrorMessage = "Invalid latitude value" };
            }

            if (!latitudeDirectionParser.TryParse(fields[1], out var latitudeDirection))
            {
                return new ParseResult<GLLMessageData> { Success = false, ErrorMessage = "Invalid latitude direction" };
            }

            if (!doubleFieldParser.TryParse(fields[2], out var longitude))
            {
                return new ParseResult<GLLMessageData> { Success = false, ErrorMessage = "Invalid longitude value" };
            }

            if (!longitudeDirectionParser.TryParse(fields[3], out var longitudeDirection))
            {
                return new ParseResult<GLLMessageData> { Success = false, ErrorMessage = "Invalid longitude direction" };
            }

            if (!dateTimeFieldParser.TryParse(fields[4], out var utcTime))
            {
                return new ParseResult<GLLMessageData> { Success = false, ErrorMessage = "Invalid UTC time value" };
            }

            if (!statusParser.TryParse(fields[5], out var status))
            {
                return new ParseResult<GLLMessageData> { Success = false, ErrorMessage = "Invalid status" };
            }

            if (!modeIndicatorParser.TryParse(fields[6], out var modeIndicator))
            {
                return new ParseResult<GLLMessageData> { Success = false, ErrorMessage = "Invalid mode indicator" };
            }

            // konwertowanie latitude i longitude do wartości dziesiętnych
            double decimalLatitude = Math.Floor(latitude / 100) + (latitude % 100) / 60;
            double decimalLongitude = Math.Floor(longitude / 100) + (longitude % 100) / 60;

            var gllMessageData = new GLLMessageData(
                decimalLatitude,
                latitudeDirection,
                decimalLongitude,
                longitudeDirection,
                utcTime,
                status,
                modeIndicator);

            var validationResult = validator.Validate(gllMessageData);

            if (!validationResult.IsValid)
            {
                return new ParseResult<GLLMessageData> { Success = false, ErrorMessage = validationResult.ToString() };
            }

            return new ParseResult<GLLMessageData> { Success = true, Data = gllMessageData };
        }
    }
}