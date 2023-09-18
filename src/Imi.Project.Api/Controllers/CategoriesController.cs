using Imi.Project.Api.Core.Dto.Category;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Core.Services;
using Imi.Project.Api.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Reflection.Metadata;

namespace Imi.Project.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IRecipeService _recipeService;

        public CategoriesController(ICategoryService categoryService, IRecipeService recipeService)
        {
            _categoryService = categoryService;
            _recipeService = recipeService;
        }

        #region GET api/categories

        [SwaggerOperation("Retrieve all categories", "Gets all categories")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var responseDto = await _categoryService.ListAllAsync();               
                return Ok(responseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }
        }

        #endregion

        #region GET api/categories/id

        [SwaggerOperation("Retrieve a category", "Gets a category by providing an id")]
        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                if (!await _categoryService.EntityExistsAsync(id))
                    return NotFound($"No category found with an id of {id}");
                        
                        
                var responseDto = await _categoryService.GetByIdAsync(id);                    
                return Ok(responseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }
        }

        #endregion

        #region POST api/categories
        [SwaggerOperation("Add a new category", "Adds a new category using the properties supplied, returns 201 with the added category")]
        [HttpPost]
        [Authorize(Policy = Constants.OnlyAdminsPolicy)]
        public async Task<IActionResult> Add(CategoryRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            try
            {                          
                var responseDto = await _categoryService.AddAsync(requestDto);
                return CreatedAtRoute("GetCategory", new { Id = responseDto.Id }, responseDto);

            }
            catch (Exception)
            {
                return StatusCode(500, "Server error while uploading data");
            }
        }

        #endregion

        #region PUT api/categories/id
        [SwaggerOperation("Edit an existing category", "Edits an existing category using the properties supplied, returns 204 No Content")]
        [SwaggerResponse(204, "Success")]
        [HttpPut("{id}")]
        [Authorize(Policy = Constants.OnlyAdminsPolicy)]
        public async Task<IActionResult> Update(Guid id, CategoryRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));


            try
            {
                if (!await _categoryService.EntityExistsAsync(id))
                    return NotFound($"No category found with an id of {id}");

                await _categoryService.UpdateAsync(id, requestDto);
                return NoContent();
            }

            catch (Exception)
            {
                return StatusCode(500, "Server error while updating data");
            }          

        }

        #endregion

        #region DELETE api/categories/id

        [SwaggerOperation("Delete a category", "Deletes a category, returns 204 No Content")]
        [SwaggerResponse(204, "Success")]
        [HttpDelete("{id}")]
        [Authorize(Policy = Constants.OnlyAdminsPolicy)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (!await _categoryService.EntityExistsAsync(id))
                    return NotFound($"No category found with an id of {id}");

                await _categoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }
        }

        #endregion

        #region GET api/categories/id/recipes

        [SwaggerOperation("Gets all recipes for a given category")]
        [HttpGet("{id}/recipes")]
        public async Task<IActionResult> GetRecipesFromCategory(Guid id)
        {
            try
            {
                if (!await _categoryService.EntityExistsAsync(id))
                    return NotFound($"No category found with an id of {id}");

                var responseDto = await _recipeService.GetByCategoryIdAsync(id);
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

