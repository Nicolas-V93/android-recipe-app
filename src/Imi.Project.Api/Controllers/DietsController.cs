using Imi.Project.Api.Core.Dto.Category;
using Imi.Project.Api.Core.Dto.Diet;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Infrastructure.Authorization;
using Imi.Project.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Imi.Project.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DietsController : Controller
    {
        private readonly IDietService _dietService;
        private readonly IRecipeService _recipeService;

        public DietsController(IDietService dietService, IRecipeService recipeService)
        {
            _dietService = dietService;
            _recipeService = recipeService;
        }

        #region GET api/diets

        [SwaggerOperation("Retrieve all diets", "Gets all diets")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var responseDto = await _dietService.ListAllAsync();
                return Ok(responseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }
        }

        #endregion

        #region GET api/diets/id

        [SwaggerOperation("Retrieve a diet", "Gets a diet by providing an id")]
        [HttpGet("{id}", Name = "GetDiet")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                if (!await _dietService.EntityExistsAsync(id))
                    return NotFound($"No diet found with an id of {id}");


                var responseDto = await _dietService.GetByIdAsync(id);
                return Ok(responseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }
        }

        #endregion

        #region POST api/diets
        [SwaggerOperation("Add a new diet", "Adds a new diet using the properties supplied, returns 201 with the added diet")]
        [HttpPost]
        [Authorize(Policy = Constants.OnlyAdminsPolicy)]
        public async Task<IActionResult> Add(DietRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            try
            {
                var responseDto = await _dietService.AddAsync(requestDto);
                return CreatedAtRoute("GetDiet", new { Id = responseDto.Id }, responseDto);

            }
            catch (Exception)
            {
                return StatusCode(500, "Server error while uploading data");
            }
        }

        #endregion

        #region PUT api/diets/id
        [SwaggerOperation("Edit an existing diet", "Edits an existing diet using the properties supplied, returns 204 No Content")]
        [SwaggerResponse(204, "Success")]
        [HttpPut("{id}")]
        [Authorize(Policy = Constants.OnlyAdminsPolicy)]
        public async Task<IActionResult> Update(Guid id, DietRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));


            try
            {
                if (!await _dietService.EntityExistsAsync(id))
                    return NotFound($"No diet found with an id of {id}");

                await _dietService.UpdateAsync(id, requestDto);
                return NoContent();
            }

            catch (Exception)
            {
                return StatusCode(500, "Server error while updating data");
            }

        }

        #endregion

        #region DELETE api/diets/id

        [SwaggerOperation("Delete a diet", "Deletes a diet, returns 204 No Content")]
        [SwaggerResponse(204, "Success")]
        [HttpDelete("{id}")]
        [Authorize(Policy = Constants.OnlyAdminsPolicy)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (!await _dietService.EntityExistsAsync(id))
                    return NotFound($"No diet found with an id of {id}");

                await _dietService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }
        }

        #endregion

        #region GET api/diets/id/recipes

        [SwaggerOperation("Gets all recipes for a given diet")]
        [HttpGet("{id}/recipes")]
        public async Task<IActionResult> GetRecipesFromDiet(Guid id)
        {
            try
            {
                if (!await _dietService.EntityExistsAsync(id))
                    return NotFound($"No diet found with an id of {id}");

                var responseDto = await _recipeService.GetByDietIdAsync(id);
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
