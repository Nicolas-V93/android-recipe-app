using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Helpers.CustomClaimTypes;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IAuthorizationService _authorizationService;

        public UserService(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public string GetUserId(ClaimsPrincipal User)
        {
            return User.FindFirstValue(CustomClaimType.Sub);
        }

        public async Task<bool> AuthorizeAsync<T>(ClaimsPrincipal user, T entity, IAuthorizationRequirement requirement)
        {
            var result = await _authorizationService.AuthorizeAsync(user, entity, requirement);

            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

    }
}
