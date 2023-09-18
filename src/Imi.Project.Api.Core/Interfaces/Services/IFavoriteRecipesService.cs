using Imi.Project.Api.Core.Dto.FavoriteRecipe;
using Imi.Project.Api.Core.Dto.Recipe;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface IFavoriteRecipesService
    {
        Task<IEnumerable<RecipeResponseDto>> GetFavoritesAsync(ClaimsPrincipal user);
        Task AddAsync(FavoriteRecipeDto requestDto, ClaimsPrincipal user);
        Task DeleteAsync(Guid id, ClaimsPrincipal user);
        Task<bool> IsBookmarked(Guid recipeId, ClaimsPrincipal user);
    }
}
