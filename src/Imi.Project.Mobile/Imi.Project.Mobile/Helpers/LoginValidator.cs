using FluentValidation;
using Imi.Project.Mobile.Models;

namespace Imi.Project.Mobile.Helpers
{
    public class LoginValidator : AbstractValidator<AuthenticationRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
                                 .EmailAddress().WithMessage("Please enter a valid email address.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
