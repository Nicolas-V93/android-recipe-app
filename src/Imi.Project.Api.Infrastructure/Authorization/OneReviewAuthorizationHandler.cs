using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Helpers.CustomClaimTypes;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Infrastructure.Authorization
{
    public class OneReviewAuthorizationHandler : AuthorizationHandler<OneReviewAuthorizationRequirement, Recipe>
    {
        private readonly IReviewRepository _reviewRepository;

        public OneReviewAuthorizationHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OneReviewAuthorizationRequirement requirement,
            Recipe recipe)
        {
            if (context.User == null)
            {
                return;
            }

            if (!await RecipeHasUserReview(recipe, context.User) || isAdmin(context.User))
            {
                context.Succeed(requirement);
            }

            return;
        }

        private async Task<bool> RecipeHasUserReview(Recipe recipe, ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(CustomClaimType.Sub);
            return await _reviewRepository.GetAll()
                                    .AnyAsync(re => re.RecipeId.Equals(recipe.Id) && re.ApplicationUserId.Equals(userId));
        }

        private bool isAdmin(ClaimsPrincipal user)
        {
            return user.HasClaim(ClaimTypes.Role, Constants.AdministratorRole);
        }
    }
}
