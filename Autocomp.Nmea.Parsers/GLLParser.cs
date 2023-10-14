using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;
using Autocomp.Nmea.Parsers.Interfaces;
using Autocomp.Nmea.Parsers.Utilities;
using FluentValidation;
using static Autocomp.Nmea.Models.NmeaEnums.GLLEnums;

namespace Autocomp.Nmea.Parsers
{
    public class GLLParser : INmeaParser<GLLMessageData>
    {
        private readonly IValidator<GLLMessageData> validator;
        private readonly IFieldParser<decimal> decimalFieldParser;
        private readonly IFieldParser<DateTime> dateTimeFieldParser;
        private readonly IFieldParser<LatitudeDirection> latitudeDirectionParser;
        private readonly IFieldParser<LongitudeDirection> longitudeDirectionParser;
        private readonly IFieldParser<Status> statusParser;
        private readonly IFieldParser<ModeIndicator> modeIndicatorParser;

        public GLLParser(
            IValidator<GLLMessageData> validator,
            IFieldParser<decimal> decimalFieldParser,
            IFieldParser<DateTime> dateTimeFieldParser,
            IFieldParser<LatitudeDirection> latitudeDirectionParser,
            IFieldParser<LongitudeDirection> longitudeDirectionParser,
            IFieldParser<Status> statusParser,
            IFieldParser<ModeIndicator> modeIndicatorParser
            )
        {
            this.validator = validator;
            this.decimalFieldParser = decimalFieldParser;
            this.dateTimeFieldParser = dateTimeFieldParser;
            this.latitudeDirectionParser = latitudeDirectionParser;
            this.longitudeDirectionParser = longitudeDirectionParser;
            this.statusParser = statusParser;
            this.modeIndicatorParser = modeIndicatorParser;
        }

        /// <summary>
        /// Przetwarza wiadomość NMEA na obiekt danych GLL.
        /// </summary>
        public ParseResult<GLLMessageData> Parse(NmeaMessage message)
        {
            var fields = message.Fields;

            if (fields.Count() < 7)
            {
                return new ParseResult<GLLMessageData> { Success = false, ErrorMessage = "Some value is missing" };
            }

            if (!decimalFieldParser.TryParse(fields[0], out var latitude))
            {
                return new ParseResult<GLLMessageData> { Success = false, ErrorMessage = "Invalid latitude value" };
            }

            if (!latitudeDirectionParser.TryParse(fields[1], out var latitudeDirection))
            {
                return new ParseResult<GLLMessageData> { Success = false, ErrorMessage = "Invalid latitude direction" };
            }

            if (!decimalFieldParser.TryParse(fields[2], out var longitude))
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

            // konwertowanie latitude i longitude do dziesiętnych wartości stopni i minut kątowych po to aby
            // sprawdzić czy zakres tych wartości jest odpowwiedni. użycie decimal gwarantuje lepszą dokładność
            decimal decimalLatitude = NmeaUtilities.ConvertToDecimalDegrees(latitude);
            decimal decimalLongitude = NmeaUtilities.ConvertToDecimalDegrees(longitude);

            var gllMessageData = new GLLMessageData(
                (double)decimalLatitude,
                latitudeDirection,
                (double)decimalLongitude,
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