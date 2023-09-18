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
    public class RecipeIngredientRepository : IRecipeIngredientRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RecipeIngredientRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<RecipeIngredient> GetAll()
        {
            return _dbContext.Set<RecipeIngredient>().Include(ri => ri.Ingredient)
                                                     .Include(ri => ri.Unit);
        }

        public async Task<IEnumerable<RecipeIngredient>> GetIngredientsAsync(Guid recipeId)
        {
            return await GetAll().Where(ri => ri.RecipeId.Equals(recipeId)).ToListAsync();
        }
        public async Task<RecipeIngredient> GetIngredientAsync(Guid recipeId, Guid ingredientId)
        {
            return await GetAll().Where(ri => ri.RecipeId.Equals(recipeId) && ri.IngredientId.Equals(ingredientId))
                                 .FirstOrDefaultAsync();
        }
        public async Task<RecipeIngredient> AddAsync(RecipeIngredient recipeIngredient)
        {
            _dbContext.Set<RecipeIngredient>().Add(recipeIngredient);
            await _dbContext.SaveChangesAsync();
            return recipeIngredient;
        }
        public async Task<RecipeIngredient> UpdateAsync(RecipeIngredient recipeIngredient)
        {
            _dbContext.Set<RecipeIngredient>().Update(recipeIngredient);
            await _dbContext.SaveChangesAsync();
            return recipeIngredient;
        }
        public async Task<RecipeIngredient> DeleteAsync(RecipeIngredient recipeIngredient)
        {
            _dbContext.Set<RecipeIngredient>().Remove(recipeIngredient);
            await _dbContext.SaveChangesAsync();
            return recipeIngredient;
        }
        public async Task<bool> RecipeHasIngredientAsync(Guid recipeId, Guid ingredientId)
        {
            return await _dbContext.Set<RecipeIngredient>().AnyAsync
                (ri => ri.RecipeId.Equals(recipeId) && ri.IngredientId.Equals(ingredientId));
        }

       
    }
}
