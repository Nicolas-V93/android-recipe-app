using Imi.Project.Api.Core.Dto;
using Imi.Project.Api.Core.Dto.Instruction;
using Imi.Project.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface IInstructionService
    {
        Task<IEnumerable<InstructionResponseDto>> GetInstructionsFromRecipeAsync(Guid recipeId);
        Task<InstructionResponseDto> AddInstructionToRecipeAsync(Guid recipeId, InstructionRequestDto requestDto);
        Task UpdateInstructionAsync(Guid recipeId, Guid instructionId, InstructionRequestDto requestDto);
        Task DeleteInstructionAsync(Guid recipeId, Guid instructionId);
        Task<bool> RecipeHasInstruction(Guid recipeId, Guid instructionId);
        Task<int[]> GetStepNumbersAsync(Guid recipeId);

    }
}
