using Imi.Project.Api.Core.Dto.Instruction;
using Imi.Project.Api.Core.Dto.RecipeIngredient;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Core.Services;
using Imi.Project.Api.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Reflection.Metadata.Ecma335;

namespace Imi.Project.Api.Controllers
{
    [Route("api/recipes/{recipeId}/ingredients")]
    [Authorize]
    [ApiController]
    public class RecipeIngredientsController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly IRecipeIngredientService _recipeIngredientService;

        public RecipeIngredientsController(IRecipeService recipeService, IRecipeIngredientService recipeIngredientService)
        {
            _recipeService = recipeService;
            _recipeIngredientService = recipeIngredientService;
        }

        #region GET api/recipes/{recipeId}/ingredients
        [SwaggerOperation("Retrieve all ingredients for a given recipe")]
        [HttpGet]
        public async Task<IActionResult> GetIngredientsFromRecipe(Guid recipeId)
        {
            try
            {
                if (!await _recipeService.EntityExistsAsync(recipeId))
                    return NotFound($"No recipe found with an id of {recipeId}");

                var responseDto = await _recipeIngredientService.GetIngredientsAsync(recipeId);
                return Ok(responseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }

        }
        #endregion

        #region POST api/recipes/{recipeId}/ingredients
        [SwaggerOperation
           (Summary = "Add a new ingredient to an existing recipe",
           Description = "Adds a new ingredient using the properties supplied. \n\n Ingredient will be created when it does not yet exist. \n \n returns 201 with the added recipe"
           )]
        [SwaggerResponse(201, "Success")]
        [HttpPost]
        public async Task<IActionResult> AddIngredient(Guid recipeId, RecipeIngredientRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            try
            {
                if (!await _recipeService.EntityExistsAsync(recipeId))
                    return NotFound($"No recipe found with an id of {recipeId}");

                if (!await _recipeService.AuthorizeAsync(recipeId, User, UserOperations.Create))
                    return Forbid();

                var responseDto = await _recipeIngredientService.AddAsync(recipeId, requestDto);
                return CreatedAtRoute(new { recipeId = recipeId }, responseDto);
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

        #region PUT api/recipe/{recipeId}/ingredients/{ingredientId}
        [SwaggerOperation("Updates an ingredient for a given recipe")]
        [SwaggerResponse(204, "Success")]
        [HttpPut("{ingredientId}")]
        public async Task<IActionResult> UpdateIngredient(Guid recipeId, Guid ingredientId, RecipeIngredientUpdateRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            try
            {
                if (!await _recipeService.EntityExistsAsync(recipeId))
                    return NotFound($"No recipe found with an id of {recipeId}");

                if (!await _recipeIngredientService.RecipeHasIngredientAsync(recipeId, ingredientId))
                    return NotFound($"Recipe with id {recipeId} has no ingredient with an id of {ingredientId}");

                if (!await _recipeService.AuthorizeAsync(recipeId, User, UserOperations.Update))
                    return Forbid();

                await _recipeIngredientService.UpdateAsync(recipeId, ingredientId, requestDto);
                return NoContent();

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }
        }
        #endregion

        #region DELETE api/recipe/{recipeId}/ingredients/{ingredientId}
        [SwaggerOperation("Deletes an ingredient from a given recipe")]
        [SwaggerResponse(204, "Success")]
        [HttpDelete("{ingredientId}")]
        public async Task<IActionResult> DeleteIngredient(Guid recipeId, Guid ingredientId)
        {
            try
            {
                if (!await _recipeService.EntityExistsAsync(recipeId))
                    return NotFound($"No recipe found with an id of {recipeId}");

                if (!await _recipeIngredientService.RecipeHasIngredientAsync(recipeId, ingredientId))
                    return NotFound($"Recipe with id {recipeId} has no ingredient with an id of {ingredientId}");

                if (!await _recipeService.AuthorizeAsync(recipeId, User, UserOperations.Delete))
                    return Forbid();

                await _recipeIngredientService.DeleteAsync(recipeId, ingredientId);
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
