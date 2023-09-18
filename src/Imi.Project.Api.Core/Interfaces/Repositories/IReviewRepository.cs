using Imi.Project.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Repositories
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        Task<IEnumerable<Review>> GetReviewsFromRecipeAsync(Guid recipeId);
        Task<Review> GetReviewFromRecipeAsync(Guid recipeId, Guid reviewId);
        Task<bool> RecipeHasReview(Guid recipeId, Guid reviewId);
    }
}
