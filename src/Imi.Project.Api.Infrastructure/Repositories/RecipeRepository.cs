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
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public override IQueryable<Recipe> GetAll()
        {
            return base.GetAll().Include(r => r.Category)
                                .Include(r => r.Diet)
                                .Include(r => r.ApplicationUser);
        }
        public override async Task<IEnumerable<Recipe>> ListAllAsync()
        {
            return await GetAll().ToListAsync();
        }
        public override async Task<Recipe> GetByIdAsync(Guid id)
        {
            return await GetAll().FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<Recipe> GetByIdDetailsAsync(Guid id)
        {
            return await GetAll().Include(r => r.Instructions.OrderBy(i => i.StepNumber))
                                 .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                                 .Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Unit)
                                 .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<IEnumerable<Recipe>> GetByCategoryIdAsync(Guid id)
        {
            return await GetAll().Where(r => r.CategoryId.Equals(id)).ToListAsync();
        }
        public async Task<IEnumerable<Recipe>> GetByDietIdAsync(Guid id)
        {
            return await GetAll().Where(r => r.DietId.Equals(id)).ToListAsync();
        }
        public async Task<IEnumerable<Recipe>> GetByCurrentUserIdAsync(string userId)
        {
            return await GetAll().Where(r => r.ApplicationUserId.Equals(userId)).ToListAsync();
        }
        public async Task<IEnumerable<Recipe>> GetByIngredientIdAsync(Guid id)
        {
            return await GetAll().Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                                 .Where(r => r.RecipeIngredients.Any(ri => ri.IngredientId.Equals(id)))
                                 .ToListAsync();
        }
        public async Task<IEnumerable<Recipe>> ExecuteQueryAsync(IQueryable<Recipe> query)
        {
            return await query.ToListAsync();
        }

    }
}
