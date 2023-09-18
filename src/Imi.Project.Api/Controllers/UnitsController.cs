using Imi.Project.Api.Core.Dto.Category;
using Imi.Project.Api.Core.Dto.Unit;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Imi.Project.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitsController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        #region GET api/units

        [SwaggerOperation("Retrieve all units", "Gets all units")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var responseDto = await _unitService.ListAllAsync();
                return Ok(responseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "A server error occured");
            }
        }

        #endregion

        #region GET api/units/id

        [SwaggerOperation("Retrieve a unit", "Gets a unit by providing an id")]
        [HttpGet("{id}", Name = "GetUnit")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                if (!await _unitService.EntityExistsAsync(id))
                    return NotFound($"No unit found with an id of {id}");


                var responseDto = await _unitService.GetByIdAsync(id);
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
