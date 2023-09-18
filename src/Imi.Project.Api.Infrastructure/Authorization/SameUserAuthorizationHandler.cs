using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Helpers.CustomClaimTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Imi.Project.Api.Infrastructure.Authorization
{
    public class SameUserAuthorizationHandler : IAuthorizationHandler
    {

        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();

            foreach (var requirement in pendingRequirements)
            {
                if (requirement is OperationAuthorizationRequirement req)
                {
                    if (req.Name != Constants.CreateOperationName &&
                        req.Name != Constants.ReadOperationName &&
                        req.Name != Constants.UpdateOperationName &&
                        req.Name != Constants.DeleteOperationName &&
                        req.Name != Constants.CreateReviewOperationName)
                    {
                        return Task.CompletedTask;
                    }

                    //Disallow user to review their own recipe
                    if (context.Resource is Recipe && req.Name == Constants.CreateReviewOperationName)
                    {
                        if (IsOwner(context.User, context.Resource))
                        {
                            return Task.CompletedTask;
                        }
                        context.Succeed(requirement);
                    }

                }

                if (IsOwner(context.User, context.Resource))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }

        private bool IsOwner(ClaimsPrincipal user, object resource)
        {
            if (resource is Recipe recipe)
            {
                return user.HasClaim(CustomClaimType.Sub, recipe.ApplicationUserId);
            }

            if (resource is Review review)
            {
                return user.HasClaim(CustomClaimType.Sub, review.ApplicationUserId);
            }

            return false;
        }
    }
}
