using FluentValidation;
using Imi.Project.Mobile.Core.Models;
using System;

namespace Imi.Project.Mobile.Core.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterModel>
    {
        public RegisterValidator()
        {
            RuleFor(rar => rar.FirstName).NotEmpty().WithMessage("Please specify a first name");
            RuleFor(rar => rar.LastName).NotEmpty().WithMessage("Please specify a last name");
            RuleFor(rar => rar.UserName).NotEmpty().WithMessage("Please specify a user name");
            RuleFor(rar => rar.DateOfBirth).LessThan(DateTime.Today);
            RuleFor(rar => rar.Email).EmailAddress().WithMessage("E-mail address format is incorrect");
            RuleFor(rar => rar.Password)
                .Length(5, 50)
                .WithMessage("Password must be between 5 and 50 characters long");
            RuleFor(rar => rar.ConfirmPassword).Equal(rar => rar.Password).WithMessage("Passwords are not equal");
        }
    }
}
