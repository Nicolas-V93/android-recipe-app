using Imi.Project.Api.Core.Dto.FavoriteRecipe;
using Imi.Project.Api.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Imi.Project.Api.Controllers
{
    [Route("api/me")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly IFavoriteRecipesService _favoriteRecipesService;

        public AccountController(IRecipeService recipeService,
            IFavoriteRecipesService favoriteRecipesService)
        {
            _recipeService = recipeService;
            _favoriteRecipesService = favoriteRecipesService;
        }

        #region GET api/me/recipes
        [SwaggerOperation("Retrieve all recipes for a given user", "Gets all recipes for a given user")]
        [HttpGet("recipes")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var responseDto = await _recipeService.GetByCurrentUserAsync(User);
                return Ok(responseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }

        }
        #endregion

        #region GET api/me/favorites
        [SwaggerOperation("Retrieve all bookmarked recipes for a given user", "Gets bookmarked recipes for a given user")]
        [HttpGet("favorites")]
        public async Task<IActionResult> GetFavorites()
        {
            try
            {
                var responseDto = await _favoriteRecipesService.GetFavoritesAsync(User);
                return Ok(responseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }

        }
        #endregion

        #region POST api/me/favorites
        [SwaggerOperation
            (Summary = "Bookmark a recipe",
            Description = "Bookmarks a recipe for a user, returns 201"
            )]
        [SwaggerResponse(201, "Success")]
        [HttpPost("favorites")]
        public async Task<IActionResult> Add(FavoriteRecipeDto requestDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            try
            {
                await _favoriteRecipesService.AddAsync(requestDto, User);
                return CreatedAtAction("GetFavorites", null);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error while uploading data");
            }
        }
        #endregion

        #region DELETE api/me/favorites/recipeId

        [SwaggerOperation("Removes a recipe as bookmark", "Removes a recipe as bookmark, returns 204")]
        [SwaggerResponse(204, "Success")]
        [HttpDelete("favorites/{recipeId}")]
        public async Task<IActionResult> Delete(Guid recipeId)
        {
            try
            {
                if (!await _favoriteRecipesService.IsBookmarked(recipeId, User))
                    return NotFound($"Recipe {recipeId} is not bookmarked");

                await _favoriteRecipesService.DeleteAsync(recipeId, User);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }
        }

        #endregion
    }
}
