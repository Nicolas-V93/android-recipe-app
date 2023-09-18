using AutoMapper;
using Imi.Project.Api.Core.Dto.FavoriteRecipe;
using Imi.Project.Api.Core.Dto.Recipe;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Services
{
    public class FavoriteRecipeService : IFavoriteRecipesService
    {
        private readonly IFavoriteRecipesRepository _favoriteRecipesRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public FavoriteRecipeService(IFavoriteRecipesRepository favoriteRecipesRepository,
            IUserService userService,
            IMapper mapper)
        {
            _favoriteRecipesRepository = favoriteRecipesRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecipeResponseDto>> GetFavoritesAsync(ClaimsPrincipal user)
        {
            var userId = _userService.GetUserId(user);
            var result = await _favoriteRecipesRepository.GetFavoritesAsync(userId);
            return _mapper.Map<IEnumerable<RecipeResponseDto>>(result);
        }

        public async Task AddAsync(FavoriteRecipeDto requestDto, ClaimsPrincipal user)
        {
            if (await IsBookmarked(requestDto.RecipeId, user))
                throw new ArgumentException($"Recipe with id {requestDto.RecipeId} has already been bookmarked");

            var userId = _userService.GetUserId(user);
            var newFavorite = new FavoriteRecipe { ApplicationUserId = userId, RecipeId = requestDto.RecipeId };
            await _favoriteRecipesRepository.AddAsync(newFavorite);

        }

        public async Task DeleteAsync(Guid recipeId, ClaimsPrincipal user)
        {
            var userId = _userService.GetUserId(user);
            var bookmarked = await _favoriteRecipesRepository.GetBookmarkedRecipe(recipeId, userId);
            await _favoriteRecipesRepository.DeleteAsync(bookmarked);
        }

        public async Task<bool> IsBookmarked(Guid recipeId, ClaimsPrincipal user)
        {
            var userId = _userService.GetUserId(user);
            return await _favoriteRecipesRepository.IsBookmarked(recipeId, userId);
        }
    }
}
