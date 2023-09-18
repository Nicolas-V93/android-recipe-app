using Imi.Project.Api.Core.Dto.Instruction;
using Imi.Project.Api.Core.Dto;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;
using Imi.Project.Api.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Imi.Project.Api.Core.Services;
using Imi.Project.Api.Infrastructure.Authorization;

namespace Imi.Project.Api.Controllers
{
    [Route("api/recipes/{recipeId}/instructions")]
    [Authorize]
    [ApiController]
    public class InstructionsController : ControllerBase
    {
        private readonly IInstructionService _instructionService;
        private readonly IRecipeService _recipeService;

        public InstructionsController(IInstructionService instructionService, IRecipeService recipeService)
        {
            _instructionService = instructionService;
            _recipeService = recipeService;

        }

        #region GET api/recipes/{recipeId}/instructions
        [SwaggerOperation("Retrieve all instructions for a given recipe")]
        [HttpGet]
        public async Task<IActionResult> GetInstructions(Guid recipeId)
        {
            try
            {
                if (!await _recipeService.EntityExistsAsync(recipeId))
                    return NotFound($"No recipe found with an id of {recipeId}");

                var responseDto = await _instructionService.GetInstructionsFromRecipeAsync(recipeId);

                return Ok(responseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }

        }
        #endregion

        #region POST api/recipe/{recipeId}/instructions
        [HttpPost]
        [SwaggerOperation
            (Summary = "Add an instruction to a recipe",
            Description = "Adds a new instruction for a recipe using the properties supplied, returns 201 with the added instruction"
            )]
        [SwaggerResponse(201, "Success")]
        public async Task<IActionResult> AddInstruction(Guid recipeId, InstructionRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            try
            {
                if (!await _recipeService.EntityExistsAsync(recipeId))
                    return NotFound($"No recipe found with an id of {recipeId}");

                if (!await _recipeService.AuthorizeAsync(recipeId, User, UserOperations.Create))
                    return Forbid();

                var responseDto = await _instructionService.AddInstructionToRecipeAsync(recipeId, requestDto);

                return CreatedAtRoute(new { recipeId = recipeId }, responseDto);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }
        }
        #endregion

        #region PUT api/recipe/{recipeId}/instructions/{instructionId}
        [SwaggerOperation("Updates an instruction for a given recipe")]
        [SwaggerResponse(204, "Success")]
        [HttpPut("{instructionId}")]
        public async Task<IActionResult> UpdateInstruction(Guid recipeId, Guid instructionId, InstructionRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            try
            {
                if (!await _recipeService.EntityExistsAsync(recipeId))
                    return NotFound($"No recipe found with an id of {recipeId}");

                if(!await _instructionService.RecipeHasInstruction(recipeId, instructionId)) 
                    return NotFound($"No instruction found with an id of {instructionId} for recipe {recipeId}");

                if (!await _recipeService.AuthorizeAsync(recipeId, User, UserOperations.Update))
                    return Forbid();

                await _instructionService.UpdateInstructionAsync(recipeId, instructionId, requestDto);
                return NoContent();

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }
        }
        #endregion

        #region DELETE api/recipe/{recipeId}/instructions/{instructionId}
        [SwaggerOperation("Deletes an instruction for a given recipe")]
        [SwaggerResponse(204, "Success")]
        [HttpDelete("{instructionId}")]
        public async Task<IActionResult> DeleteInstruction(Guid recipeId, Guid instructionId)
        {
            try
            {
                if (!await _recipeService.EntityExistsAsync(recipeId))
                    return NotFound($"No recipe found with an id of {recipeId}");

                if (!await _instructionService.RecipeHasInstruction(recipeId, instructionId))
                    return NotFound($"No instruction found with an id of {instructionId} for recipe {recipeId}");

                if (!await _recipeService.AuthorizeAsync(recipeId, User, UserOperations.Delete))
                    return Forbid();

                await _instructionService.DeleteInstructionAsync(recipeId, instructionId);
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
