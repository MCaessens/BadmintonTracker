using FluentValidation;
using Imi.Project.Mobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imi.Project.Mobile.Core.Validators
{
    public class RacketsValidator : AbstractValidator<RacketModel>
    {
        public RacketsValidator()
        {
            RuleFor(r => r.Brand).NotEmpty().MinimumLength(2);
            RuleFor(r => r.Model).NotEmpty().MinimumLength(2);
        }
    }
}
