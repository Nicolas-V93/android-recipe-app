using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface IUserService
    {
        string GetUserId(ClaimsPrincipal User);
        Task<bool> AuthorizeAsync<T>(ClaimsPrincipal user, T entity, IAuthorizationRequirement requirement);
    }
}
