using Imi.Project.Api.Core.Dto.RecipeIngredient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface IRecipeIngredientService
    {
        Task<IEnumerable<RecipeIngredientResponseDto>> GetIngredientsAsync(Guid recipeId);
        Task<RecipeIngredientResponseDto> AddAsync(Guid recipeId, RecipeIngredientRequestDto requestDto);
        Task UpdateAsync(Guid recipeId, Guid ingredientId, RecipeIngredientUpdateRequestDto requestDto);
        Task DeleteAsync(Guid recipeId, Guid ingredientId);
        Task<bool> RecipeHasIngredientAsync(Guid recipeId, Guid ingredientId);

    }
}

