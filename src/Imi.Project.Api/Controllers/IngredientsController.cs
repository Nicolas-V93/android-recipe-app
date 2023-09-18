using Imi.Project.Api.Core.Dto.Category;
using Imi.Project.Api.Core.Dto.Ingredient;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Core.Services;
using Imi.Project.Api.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Imi.Project.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        #region GET api/ingredients

        [SwaggerOperation("Retrieve all ingredients", "Gets all ingredients")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var responseDto = await _ingredientService.ListAllAsync();
                return Ok(responseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }
        }

        #endregion

        #region GET api/ingredients/id

        [SwaggerOperation("Retrieve an ingredient", "Gets an ingredient by providing an id")]
        [HttpGet("{id}", Name = "GetIngredient")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                if (!await _ingredientService.EntityExistsAsync(id))
                    return NotFound($"No ingredient found with an id of {id}");


                var responseDto = await _ingredientService.GetByIdAsync(id);
                return Ok(responseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }
        }

        #endregion

        #region POST api/ingredients
        [SwaggerOperation("Add a new ingredient", "Adds a new ingredient using the properties supplied, returns 201 with the added ingredient")]
        [HttpPost]
        [Authorize(Policy = Constants.OnlyAdminsPolicy)]
        public async Task<IActionResult> Add(IngredientRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            try
            {
                var responseDto = await _ingredientService.AddAsync(requestDto);
                return CreatedAtRoute("GetIngredient", new { Id = responseDto.Id }, responseDto);

            }
            catch (Exception)
            {
                return StatusCode(500, "Server error while uploading data");
            }
        }

        #endregion

        #region PUT api/ingredients/id
        [SwaggerOperation("Edit an existing ingredient", "Edits an existing ingredient using the properties supplied, returns 204 No Content")]
        [SwaggerResponse(204, "Success")]
        [HttpPut("{id}")]
        [Authorize(Policy = Constants.OnlyAdminsPolicy)]
        public async Task<IActionResult> Update(Guid id, IngredientRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));


            try
            {
                if (!await _ingredientService.EntityExistsAsync(id))
                    return NotFound($"No ingredient found with an id of {id}");

                await _ingredientService.UpdateAsync(id, requestDto);
                return NoContent();
            }

            catch (Exception)
            {
                return StatusCode(500, "Server error while updating data");
            }

        }

        #endregion

        #region DELETE api/ingredients/id

        [SwaggerOperation("Delete an ingredient", "Deletes an ingredient, returns 204 No Content")]
        [SwaggerResponse(204, "Success")]
        [HttpDelete("{id}")]
        [Authorize(Policy = Constants.OnlyAdminsPolicy)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (!await _ingredientService.EntityExistsAsync(id))
                    return NotFound($"No ingredient found with an id of {id}");

                await _ingredientService.DeleteAsync(id);
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
