using Imi.Project.Api.Core.Dto.Diet;
using Imi.Project.Api.Core.Dto.Ingredient;
using Imi.Project.Api.Core.Dto.Recipe;
using Imi.Project.Api.Core.Dto.RecipeIngredient;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Helpers.CustomClaimTypes;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Core.Services;
using Imi.Project.Api.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.Security.Claims;

namespace Imi.Project.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        #region GET api/recipes
        [SwaggerOperation("Retrieve all recipes", "Gets all recipes \n\n Search for a recipe by providing (a portion of) the title in the query " +
            "\n \n Add a value to totalTime which retrieves recipes with a max combined preparation and cooking time" )]

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? title, [FromQuery] int? totalTime)
        {
            try
            {
                var responseDto = await _recipeService.ListAllAsync(title, totalTime);
                return Ok(responseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }

        }
        #endregion

        #region GET api/recipes/id

        [SwaggerOperation("Retrieve a recipe", "Gets a recipe by providing an id")]
        [HttpGet("{id}", Name = "GetRecipe")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                if (!await _recipeService.EntityExistsAsync(id))
                    return NotFound($"No recipe found with an id of {id}");


                var responseDto = await _recipeService.GetByIdAsync(id);
                return Ok(responseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }
        }

        #endregion

        #region POST api/recipes
        [SwaggerOperation
            (Summary = "Add a new recipe", 
            Description = "Adds a new recipe using the properties supplied, returns 201 with the added recipe"
            )]
        [SwaggerResponse(201, "Success")]
        [HttpPost]
        public async Task<IActionResult> Add(RecipeRequestDto requestDto)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            try
            {
                var responseDto = await _recipeService.AddAsync(requestDto, User);
                return CreatedAtRoute("GetRecipe", new { Id = responseDto.Id }, responseDto);

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

        #region PUT api/recipes/id
        [SwaggerOperation("Edit an existing recipe", "Edits an existing recipe using the properties supplied, returns 204 No Content")]
        [SwaggerResponse(204, "Success")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, RecipeRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            try
            {
                if (!await _recipeService.EntityExistsAsync(id))
                    return NotFound($"No recipe found with an id of {id}");

                if (!await _recipeService.AuthorizeAsync(id, User, UserOperations.Update))
                    return Forbid();

                await _recipeService.UpdateAsync(id, requestDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error while updating data");
            }

        }

        #endregion

        #region DELETE api/recipes/id

        [SwaggerOperation("Delete a recipe", "Deletes a recipe, returns 204 No Content")]
        [SwaggerResponse(204, "Success")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (!await _recipeService.EntityExistsAsync(id))
                    return NotFound($"No recipe found with an id of {id}");

                if (!await _recipeService.AuthorizeAsync(id, User, UserOperations.Delete))
                    return Forbid();

                await _recipeService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }
        }

        #endregion

        #region GET api/recipes/findByIngredient
        [SwaggerOperation("Find recipes by ingredient", "Ingredient must be an exact match: ex. \"Firm Tofu\" ")]
        [Route("findByIngredient")]
        [HttpGet]
        public async Task<IActionResult> FindByIngedrient([FromQuery] string ingredient)
        {
            try
            {
                var responseDto = await _recipeService.GetByIngredientAsync(ingredient);
                return Ok(responseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }

        }
        #endregion

        #region GET api/recipes/id/information

        [SwaggerOperation("Retrieve detailed information of a recipe", "Gets a recipe (including ingredients and instructions) by providing an id")]       
        [HttpGet("{id}/information")]
        public async Task<IActionResult> GetRecipeInformation(Guid id)
        {
            try
            {
                if (!await _recipeService.EntityExistsAsync(id))
                    return NotFound($"No recipe found with an id of {id}");


                var responseDto = await _recipeService.GetRecipeInformation(id);
                return Ok(responseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }
        }

        #endregion
    }
}
