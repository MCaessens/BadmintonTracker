using FluentValidation;
using Imi.Project.Mobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imi.Project.Mobile.Core.Validators
{
    public class GamesValidator : AbstractValidator<GameModel>
    {
        public GamesValidator()
        {
            RuleFor(g => g.Opponent).NotEmpty();
        }
    }
}
