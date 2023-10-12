using Autocomp.Nmea.Models;
using FluentValidation;

namespace Autocomp.Nmea.Parsers.Validators
{
    public class MWVMessageDataValidator : AbstractValidator<MWVMessageData>
    {
        public MWVMessageDataValidator()
        {
            RuleFor(x => x.WindAngle).InclusiveBetween(0, 360)
                .WithMessage("Wind angle is out of range");

            RuleFor(x => x.WindSpeed).GreaterThanOrEqualTo(0)
                .WithMessage("Wind speed must be greater than or equal to 0");
        }
    }
}