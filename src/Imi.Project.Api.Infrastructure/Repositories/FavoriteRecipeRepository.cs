using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imi.Project.Api.Infrastructure.Repositories
{
    public class FavoriteRecipeRepository : IFavoriteRecipesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FavoriteRecipeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<FavoriteRecipe> GetAll()
        {
            return _dbContext.Set<FavoriteRecipe>().Include(fr => fr.Recipe).ThenInclude(r => r.Category)
                                                   .Include(fr => fr.Recipe).ThenInclude(r => r.Diet)
                                                   .Include(fr => fr.Recipe).ThenInclude(r => r.ApplicationUser);
        }

        public async Task<FavoriteRecipe> GetBookmarkedRecipe(Guid recipeId, string userId)
        {
            return await GetAll().Where(fr => fr.RecipeId.Equals(recipeId) && fr.ApplicationUserId.Equals(userId))
                                 .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Recipe>> GetFavoritesAsync(string userId)
        {
            return await GetAll()
                .Where(fr => fr.ApplicationUserId.Equals(userId))
                .Select(fr => fr.Recipe)
                .ToListAsync();
        }

        public async Task AddAsync(FavoriteRecipe entity)
        {
            _dbContext.Set<FavoriteRecipe>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(FavoriteRecipe entity)
        {
            _dbContext.Set<FavoriteRecipe>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsBookmarked(Guid recipeId, string userId)
        {
            return await _dbContext.Set<FavoriteRecipe>().AnyAsync
                (ri => ri.RecipeId.Equals(recipeId) && ri.ApplicationUserId.Equals(userId));
        }

    }
}
