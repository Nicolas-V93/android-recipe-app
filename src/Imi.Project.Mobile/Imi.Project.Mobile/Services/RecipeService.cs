using Imi.Project.Mobile.Dto;
using Imi.Project.Mobile.Helpers;
using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Mobile.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IAuthenticationService _authenticationService;

        public RecipeService(IBaseRepository baseRepository,
            IAuthenticationService authenticationService)
        {
            _baseRepository = baseRepository;
            _authenticationService = authenticationService;
        }

        public async Task<Recipe> UpdateRecipe(Recipe recipe)
        {
            var authToken = await _authenticationService.GetAuthToken();

            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = $"{Constants.API.RecipesEndpoint}/{recipe.Id}",
            };

            var updatedRecipe = await _baseRepository.PutAsync(recipe, builder.ToString(), authToken);
            return updatedRecipe;
        }
        public async Task<Recipe> AddRecipe(Recipe recipe)
        {
            var authToken = await _authenticationService.GetAuthToken();

            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = Constants.API.RecipesEndpoint,
            };

            var addedRecipe = await _baseRepository.PostAsync(recipe, builder.ToString(), authToken);
            return addedRecipe;
        }
        public async Task DeleteRecipe(Guid recipeId)
        {
            var authToken = await _authenticationService.GetAuthToken();

            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = $"{Constants.API.RecipesEndpoint}/{recipeId}",
            };

            await _baseRepository.DeleteAsync(recipeId, builder.ToString(), authToken);
        }
        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            var authToken = await _authenticationService.GetAuthToken();

            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = Constants.API.RecipesEndpoint,
            };

            var result = await _baseRepository.GetAllAsync<Recipe>(builder.ToString(), authToken);
            return result;
        }
        public async Task<IEnumerable<Ingredient>> GetRecipeIngredients(Guid recipeId)
        {
            var authToken = await _authenticationService.GetAuthToken();

            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = $"{Constants.API.RecipesEndpoint}/{recipeId}/ingredients"
            };

            var result = (await _baseRepository.GetAllAsync<Ingredient>(builder.ToString(), authToken));
            return result;
        }
        public async Task<IEnumerable<Instruction>> GetRecipeInstructions(Guid recipeId)
        {
            var authToken = await _authenticationService.GetAuthToken();

            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = $"{Constants.API.RecipesEndpoint}/{recipeId}/instructions"
            };

            var result = (await _baseRepository.GetAllAsync<Instruction>(builder.ToString(), authToken));
            return result;
        }
        public async Task<IEnumerable<Review>> GetRecipeReviews(Guid recipeId)
        {
            var authToken = await _authenticationService.GetAuthToken();

            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = $"{Constants.API.RecipesEndpoint}/{recipeId}/reviews"
            };

            var result = (await _baseRepository.GetAllAsync<Review>(builder.ToString(), authToken));
            return result;
        }
        public async Task<IEnumerable<Recipe>> GetUserRecipesAsync()
        {
            var authToken = await _authenticationService.GetAuthToken();

            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = $"{Constants.API.AccountEndpoint}/recipes"
            };

            var result = (await _baseRepository.GetAllAsync<Recipe>(builder.ToString(), authToken));
            return result;
        }
        public async Task<IEnumerable<Recipe>> GetBookmarkedRecipes()
        {
            var authToken = await _authenticationService.GetAuthToken();

            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = $"{Constants.API.AccountEndpoint}/favorites"
            };

            var result = (await _baseRepository.GetAllAsync<Recipe>(builder.ToString(), authToken));
            return result;
        }
        public async Task AddBookmark(Guid recipeId)
        {
            var authToken = await _authenticationService.GetAuthToken();

            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = $"{Constants.API.AccountEndpoint}/favorites"
            };

            var bookmarkDto = new BookmarkDto { RecipeId = recipeId };

            await _baseRepository.PostAsync(bookmarkDto, builder.ToString(), authToken);
        }
        public async Task RemoveBookmark(Guid recipeId)
        {
            var authToken = await _authenticationService.GetAuthToken();

            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = $"{Constants.API.AccountEndpoint}/favorites/{recipeId}"
            };

            await _baseRepository.DeleteAsync(recipeId, builder.ToString(), authToken);
        }
        public async Task<Ingredient> AddIngredient(Guid recipeId, Ingredient ingredient)
        {
            var authToken = await _authenticationService.GetAuthToken();

            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = $"{Constants.API.RecipesEndpoint}/{recipeId}/ingredients"
            };

            var result = await _baseRepository.PostAsync(ingredient, builder.ToString(), authToken);
            return result;
        }
        public async Task<Instruction> AddInstruction(Guid recipeId, Instruction instruction)
        {
            var authToken = await _authenticationService.GetAuthToken();

            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = $"{Constants.API.RecipesEndpoint}/{recipeId}/instructions"
            };

            var result = await _baseRepository.PostAsync(instruction, builder.ToString(), authToken);
            return result;
        }

    }
}
