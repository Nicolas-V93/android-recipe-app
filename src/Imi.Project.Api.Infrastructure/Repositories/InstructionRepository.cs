using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Infrastructure.Repositories
{
    public class InstructionRepository : BaseRepository<Instruction>, IInstructionRepository
    {

        public InstructionRepository(ApplicationDbContext dbContext) : base(dbContext) { }


        public async Task<IEnumerable<Instruction>> GetInstructionsFromRecipeAsync(Guid recipeId)
        {
            return await base.GetAll().Where(i => i.RecipeId.Equals(recipeId))
                                      .OrderBy(i => i.StepNumber).ToListAsync();
        }
        public async Task<Instruction> GetInstructionFromRecipeAsync(Guid recipeId, Guid instructionId)
        {
            return await GetAll().Where(i => i.RecipeId.Equals(recipeId) && i.Id.Equals(instructionId))
                                 .FirstOrDefaultAsync();
        }

        public async Task<bool> RecipeHasInstruction(Guid recipeId, Guid instructionId)
        {
            return await GetAll().AnyAsync(i => i.RecipeId.Equals(recipeId) && i.Id.Equals(instructionId));
        }
    } 
}
