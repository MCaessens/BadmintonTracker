using FluentValidation;
using Imi.Project.Mobile.Core.Models;

namespace Imi.Project.Mobile.Core.Validators
{
    public class LocationsValidator : AbstractValidator<LocationModel>
    {
        public LocationsValidator()
        {
            RuleFor(l => l.Name).NotEmpty().WithMessage("Name field cannot be empty!");
            RuleFor(l => l.Longitude).NotEmpty().WithMessage("Longitude field cannot be empty!");
            RuleFor(l => l.City).NotEmpty().WithMessage("City field cannot be empty!");
            RuleFor(l => l.Latitude).NotEmpty().WithMessage("Latitude field cannot be empty!");
            RuleFor(l => l.Street).NotEmpty().WithMessage("Street field cannot be empty!");
            RuleFor(l => l.PostalCode).NotEmpty().WithMessage("Postalcode field cannot be empty!");
        }
    }
}
