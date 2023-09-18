using FluentValidation;
using Imi.Project.Mobile.Models;

namespace Imi.Project.Mobile.Helpers
{
    public class IngredientValidator : AbstractValidator<Ingredient>
    {
        public IngredientValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Name can't be longer than 50 characters");

            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");
            RuleFor(x => x.Amount).LessThan(int.MaxValue).WithMessage($"Amount must be lesser than {int.MaxValue}");

            RuleFor(x => x.Unit).NotNull().WithMessage("Unit is required.");

        }
    }
}

