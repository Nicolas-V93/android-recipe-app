using Imi.Project.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Mobile.Interfaces
{
    public interface IRecipeService
    {
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task<IEnumerable<Recipe>> GetBookmarkedRecipes();
        Task AddBookmark(Guid recipeId);
        Task RemoveBookmark(Guid recipeId);
        Task<IEnumerable<Recipe>> GetUserRecipesAsync();
        Task<IEnumerable<Ingredient>> GetRecipeIngredients(Guid recipeId);
        Task<IEnumerable<Instruction>> GetRecipeInstructions(Guid recipeId);
        Task<IEnumerable<Review>> GetRecipeReviews(Guid recipeId);
        Task<Recipe> AddRecipe(Recipe recipe);
        Task DeleteRecipe(Guid recipeId);
        Task<Recipe> UpdateRecipe(Recipe recipe);
        Task<Ingredient> AddIngredient(Guid recipeId, Ingredient ingredient);
        Task<Instruction> AddInstruction(Guid recipeId, Instruction instruction);
    }
}
