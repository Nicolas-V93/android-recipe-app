using Imi.Project.Api.Core.Dto.Instruction;
using Imi.Project.Api.Core.Dto.Review;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponseDto>> GetReviewsFromRecipeAsync(Guid recipeId);
        Task<ReviewResponseDto> AddReviewToRecipeAsync(Guid recipeId, ClaimsPrincipal User, ReviewRequestDto requestDto);
        Task UpdateReviewAsync(Guid recipeId, Guid reviewId, ReviewRequestDto requestDto);
        Task DeleteReviewAsync(Guid recipeId, Guid reviewId);
        Task<bool> RecipeHasReview(Guid recipeId, Guid reviewId);
        Task<bool> AuthorizeAsync(Guid reviewId, ClaimsPrincipal user, OperationAuthorizationRequirement requirement);

    }
}
