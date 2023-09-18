using Imi.Project.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Repositories
{
    public interface IFavoriteRecipesRepository
    {
        Task<IEnumerable<Recipe>> GetFavoritesAsync(string userId);
        Task<FavoriteRecipe> GetBookmarkedRecipe(Guid recipeId, string userId);
        Task AddAsync(FavoriteRecipe entity);
        Task DeleteAsync(FavoriteRecipe entity);
        Task<bool> IsBookmarked(Guid recipeId, string userId);
    }
}
