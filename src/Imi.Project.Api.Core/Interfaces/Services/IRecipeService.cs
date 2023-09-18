using Imi.Project.Api.Core.Dto.Recipe;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeResponseDto>> ListAllAsync(string? title, int? totalTime);
        Task<RecipeResponseDto> GetByIdAsync(Guid id);
        Task<RecipeResponseDto> AddAsync(RecipeRequestDto requestDto, ClaimsPrincipal user);
        Task UpdateAsync(Guid id, RecipeRequestDto requestDto);
        Task DeleteAsync(Guid id);
        Task<bool> EntityExistsAsync(Guid id);
        Task<IEnumerable<RecipeResponseDto>> GetByCategoryIdAsync(Guid id);
        Task<IEnumerable<RecipeResponseDto>> GetByDietIdAsync(Guid id);
        Task<IEnumerable<RecipeResponseDto>> GetByCurrentUserAsync(ClaimsPrincipal user);
        Task<IEnumerable<RecipeResponseDto>> GetByIngredientAsync(string name);
        Task<RecipeDetailsResponseDto> GetRecipeInformation(Guid id);
        Task<bool> AuthorizeAsync(Guid id, ClaimsPrincipal user, IAuthorizationRequirement requirement);
    }
}
