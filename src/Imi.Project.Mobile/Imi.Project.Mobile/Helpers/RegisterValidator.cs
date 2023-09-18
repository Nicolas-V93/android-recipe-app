using FluentValidation;
using Imi.Project.Mobile.Models;
using System;

namespace Imi.Project.Mobile.Helpers
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
                                 .EmailAddress().WithMessage("Please enter a valid email address.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                                    .MinimumLength(6).WithMessage("Password must be at least 8 characters long.")
                                    .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                                    .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                                    .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.")
                                    .Equal(x => x.ConfirmPassword)
                                    .WithMessage("Passwords do not match.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm your password");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Select a date")
                                       .Must(dateTime => dateTime.Date <= DateTime.Now.Date).WithMessage("Date cannot be greater than today");
            RuleFor(x => x.TermsAccepted).Must(x => x == true).WithMessage("You must agree to the terms and conditions");
        }
    }
}
