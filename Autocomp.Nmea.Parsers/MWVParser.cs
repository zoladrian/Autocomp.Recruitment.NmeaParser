using Autocomp.Nmea.Common;
using Autocomp.Nmea.Models;
using Autocomp.Nmea.Parsers.Interfaces;
using FluentValidation;
using static Autocomp.Nmea.Models.NmeaEnums.MWVEnums;

namespace Autocomp.Nmea.Parsers
{
    public class MWVParser : INmeaParser<MWVMessageData>
    {
        private readonly IValidator<MWVMessageData> validator;
        private readonly IFieldParser<double> doubleFieldParser;
        private readonly IFieldParser<WindSpeedUnits> windSpeedUnitsParser;
        private readonly IFieldParser<Reference> referenceParser;
        private readonly IFieldParser<Status> statusParser;

        public MWVParser(
            IValidator<MWVMessageData> validator,
            IFieldParser<double> doubleFieldParser,
            IFieldParser<WindSpeedUnits> windSpeedUnitsParser,
            IFieldParser<Reference> referenceParser,
            IFieldParser<Status> statusParser)
        {
            this.validator = validator;
            this.doubleFieldParser = doubleFieldParser;
            this.windSpeedUnitsParser = windSpeedUnitsParser;
            this.referenceParser = referenceParser;
            this.statusParser = statusParser;
        }

        /// <summary>
        /// Przetwarza wiadomość NMEA na obiekt danych MWV.
        /// </summary>
        public ParseResult<MWVMessageData> Parse(NmeaMessage message)
        {
            var fields = message.Fields;

            if (fields.Count() < 5)
            {
                return new ParseResult<MWVMessageData> { Success = false, ErrorMessage = "Some value is missing" };
            }

            if (!doubleFieldParser.TryParse(fields[0], out var windAngle))
            {
                return new ParseResult<MWVMessageData> { Success = false, ErrorMessage = "Invalid wind angle" };
            }

            if (!referenceParser.TryParse(fields[1], out var reference))
            {
                return new ParseResult<MWVMessageData> { Success = false, ErrorMessage = "Invalid reference" };
            }

            if (!doubleFieldParser.TryParse(fields[2], out var windSpeed))
            {
                return new ParseResult<MWVMessageData> { Success = false, ErrorMessage = "Invalid wind speed" };
            }

            if (!windSpeedUnitsParser.TryParse(fields[3], out var windSpeedUnits))
            {
                return new ParseResult<MWVMessageData> { Success = false, ErrorMessage = "Invalid wind speed units" };
            }

            if (!statusParser.TryParse(fields[4], out var status))
            {
                return new ParseResult<MWVMessageData> { Success = false, ErrorMessage = "Invalid status" };
            }

            var mwvMessageData = new MWVMessageData(
                windAngle,
                reference,
                windSpeed,
                windSpeedUnits,
                status);

            var validationResult = validator.Validate(mwvMessageData);

            if (!validationResult.IsValid)
            {
                return new ParseResult<MWVMessageData> { Success = false, ErrorMessage = validationResult.ToString() };
            }

            return new ParseResult<MWVMessageData> { Success = true, Data = mwvMessageData };
        }
    }
}