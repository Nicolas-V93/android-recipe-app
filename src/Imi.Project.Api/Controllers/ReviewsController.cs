using Imi.Project.Api.Core.Dto.Instruction;
using Imi.Project.Api.Core.Dto.Review;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace Imi.Project.Api.Controllers
{
    [Route("api/recipes/{recipeId}/reviews")]
    [Authorize]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IRecipeService _recipeService;

        public ReviewsController(IReviewService reviewService, IRecipeService recipeService)
        {
            _reviewService = reviewService;
            _recipeService = recipeService;
        }

        #region GET api/recipes/{recipeId}/reviews
        [SwaggerOperation("Retrieve all reviews for a given recipe")]
        [HttpGet]
        public async Task<IActionResult> GetReviews(Guid recipeId)
        {
            try
            {
                if (!await _recipeService.EntityExistsAsync(recipeId))
                    return NotFound($"No recipe found with an id of {recipeId}");

                var responseDto = await _reviewService.GetReviewsFromRecipeAsync(recipeId);

                return Ok(responseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }

        }
        #endregion

        #region POST api/recipe/{recipeId}/reviews
        [HttpPost]
        [SwaggerOperation
            (Summary = "Add a review to a recipe",
            Description = "Adds a new review for a recipe using the properties supplied, returns 201 with the added review"
            )]
        [SwaggerResponse(201, "Success")]
        public async Task<IActionResult> AddInstruction(Guid recipeId, ReviewRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            try
            {
                if (!await _recipeService.EntityExistsAsync(recipeId))
                    return NotFound($"No recipe found with an id of {recipeId}");

                if (!await _recipeService.AuthorizeAsync(recipeId, User, UserOperations.CreateReview))
                    return Problem(
                        type: "/docs/errors/forbidden",
                        title: "Authenticated user is not authorized.",
                        detail: $"{User.FindFirstValue(ClaimTypes.Name)} can not review its own recipe",
                        statusCode: StatusCodes.Status403Forbidden,
                        instance: HttpContext.Request.Path
                    );

                if (!await _recipeService.AuthorizeAsync(recipeId, User, new OneReviewAuthorizationRequirement()))
                    return Problem(
                        type: "/docs/errors/forbidden",
                        title: "Authenticated user is not authorized.",
                        detail: $"{User.FindFirstValue(ClaimTypes.Name)} can not post more than one review for a given recipe",
                        statusCode: StatusCodes.Status403Forbidden,
                        instance: HttpContext.Request.Path
                    );

                var responseDto = await _reviewService.AddReviewToRecipeAsync(recipeId, User, requestDto);

                return CreatedAtRoute(new { recipeId = recipeId }, responseDto);
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

        #region PUT api/recipe/{recipeId}/reviews/{reviewId}
        [SwaggerOperation("Updates a review for a given recipe")]
        [SwaggerResponse(204, "Success")]
        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateInstruction(Guid recipeId, Guid reviewId, ReviewRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            try
            {
                if (!await _recipeService.EntityExistsAsync(recipeId))
                    return NotFound($"No recipe found with an id of {recipeId}");

                if (!await _reviewService.RecipeHasReview(recipeId, reviewId))
                    return NotFound($"No review found with an id of {reviewId} for recipe {recipeId}");

                if (!await _reviewService.AuthorizeAsync(reviewId, User, UserOperations.Update))
                    return Forbid();

                await _reviewService.UpdateReviewAsync(recipeId, reviewId, requestDto);
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

        #region DELETE api/recipe/{recipeId}/reviews/{reviewId}
        [SwaggerOperation("Deletes a review for a given recipe")]
        [SwaggerResponse(204, "Success")]
        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteInstruction(Guid recipeId, Guid reviewId)
        {
            try
            {
                if (!await _recipeService.EntityExistsAsync(recipeId))
                    return NotFound($"No recipe found with an id of {recipeId}");

                if (!await _reviewService.RecipeHasReview(recipeId, reviewId))
                    return NotFound($"No review found with an id of {reviewId} for recipe {recipeId}");

                if (!await _reviewService.AuthorizeAsync(reviewId, User, UserOperations.Delete))
                    return Forbid();

                await _reviewService.DeleteReviewAsync(recipeId, reviewId);
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
