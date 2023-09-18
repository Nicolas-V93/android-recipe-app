using FluentValidation;
using Imi.Project.Mobile.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace Imi.Project.Mobile.Helpers
{
    public class InstructionValidator : AbstractValidator<Instruction>
    {

        public InstructionValidator(ObservableCollection<Instruction> existingInstructions)
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Description).MaximumLength(250).WithMessage("Description can't be longer than 250 characters");

            RuleFor(x => x.StepNumber).GreaterThan(0).WithMessage("Step number must be between 1 and 20");
            RuleFor(x => x.StepNumber).LessThan(21).WithMessage("Step number must be between 1 and 20");

            RuleFor(x => x.StepNumber)
            .Must((instruction, stepNumber) => IsUniqueStepNumber(stepNumber, existingInstructions))
            .WithMessage("Step number must be unique.");
        }

        private bool IsUniqueStepNumber(int stepNumber, ObservableCollection<Instruction> existingInstructions)
        {
            return !existingInstructions.Any(i => i.StepNumber == stepNumber);
        }
    }
}
