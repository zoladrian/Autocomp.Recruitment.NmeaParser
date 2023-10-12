using Autocomp.Nmea.Models;
using FluentValidation;

namespace Autocomp.Nmea.Parsers.Validators
{
    public class GLLMessageDataValidator : AbstractValidator<GLLMessageData>
    {
        public GLLMessageDataValidator()
        {
            RuleFor(x => x.Latitude).InclusiveBetween(-90, 90).WithMessage("Latitude is out of range");
            RuleFor(x => x.Longitude).InclusiveBetween(-180, 180).WithMessage("Longitude is out of range");
        }
    }
}