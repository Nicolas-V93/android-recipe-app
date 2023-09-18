using FluentValidation;
using Imi.Project.Mobile.Models;


namespace Imi.Project.Mobile.Helpers
{
    public class RecipeValidator : AbstractValidator<Recipe>
    {
        public RecipeValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Title).MaximumLength(50).WithMessage("Title can't be longer than 50 characters");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Description).MaximumLength(250).WithMessage("Description can't be longer than 250 characters");

            RuleFor(x => x.CookTime).GreaterThan(0).WithMessage("Cooking time must be greater than zero.");
            RuleFor(x => x.CookTime).LessThan(1000).WithMessage("Cooking time must be lesser than 999.");

            RuleFor(x => x.PrepTime).GreaterThan(0).WithMessage("Preparation time must be greater than zero.");
            RuleFor(x => x.PrepTime).LessThan(1000).WithMessage("Preparation time must be lesser than 999.");

            RuleFor(x => x.Servings).GreaterThan(0).WithMessage("Servings must be greater than zero.");
            RuleFor(x => x.Servings).LessThan(31).WithMessage("Servings must be equal or less than 30.");

            RuleFor(x => x.Diet).NotNull().WithMessage("Diet is required.");
            RuleFor(x => x.Category).NotNull().WithMessage("Category is required.");
        }
    }
}
