using Imi.Project.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Repositories
{
    public interface IRecipeIngredientRepository
    {
        IQueryable<RecipeIngredient> GetAll();
        Task<IEnumerable<RecipeIngredient>> GetIngredientsAsync(Guid recipeId);
        Task<RecipeIngredient> GetIngredientAsync(Guid recipeId, Guid ingredientId);
        Task<RecipeIngredient> AddAsync(RecipeIngredient recipeIngredient);
        Task<RecipeIngredient> UpdateAsync(RecipeIngredient recipeIngredient);
        Task<RecipeIngredient> DeleteAsync(RecipeIngredient recipeIngredient);
        Task<bool> RecipeHasIngredientAsync(Guid recipeId, Guid ingredientId);
    }
}
