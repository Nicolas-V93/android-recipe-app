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
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public override IQueryable<Review> GetAll()
        {
            return base.GetAll().Include(r => r.ApplicationUser);
        }

        public ReviewRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Review>> GetReviewsFromRecipeAsync(Guid recipeId)
        {
            return await GetAll().Where(r => r.RecipeId.Equals(recipeId)).ToListAsync();
        }
        public async Task<Review> GetReviewFromRecipeAsync(Guid recipeId, Guid reviewId)
        {
            return await GetAll().Where(r => r.RecipeId.Equals(recipeId) && r.Id.Equals(reviewId))
                                 .FirstOrDefaultAsync();
        }

        public async Task<bool> RecipeHasReview(Guid recipeId, Guid reviewId)
        {
            return await GetAll().AnyAsync(r => r.RecipeId.Equals(recipeId) && r.Id.Equals(reviewId));
        }
    }
}
