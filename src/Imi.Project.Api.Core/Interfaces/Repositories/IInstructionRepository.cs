using Imi.Project.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Repositories
{
    public interface IInstructionRepository : IBaseRepository<Instruction>
    {
        Task<IEnumerable<Instruction>> GetInstructionsFromRecipeAsync(Guid recipeId);       
        Task<Instruction> GetInstructionFromRecipeAsync(Guid recipeId, Guid instructionId);
        Task<bool> RecipeHasInstruction(Guid recipeId, Guid instructionId);
    }
}
