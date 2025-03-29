using System.Net;
using CalculateTaxes.Data.Exception;
using CalculateTaxes.Domain.Dtos.FeatureFlag;
using CalculateTaxes.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CalculateTaxes.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class FeatureFlagController(IFeatureFlagService service, ILogger<ClientController> logger) : ControllerBase
    {
        private readonly IFeatureFlagService _service = service;
        private readonly ILogger<ClientController> _logger = logger;

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeatureFlagResponse))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdFeatureFlag([FromQuery] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.GetByIdFeatureFlag(id);

                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                _logger.LogError("{error}", JsonConvert.SerializeObject(e));
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("{error}", JsonConvert.SerializeObject(e));
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("Name")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeatureFlagResponse))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByNameFeatureFlag([FromQuery] string Name)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.GetByNameFeatureFlag(Name);

                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                _logger.LogError("{error}", JsonConvert.SerializeObject(e));
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("{error}", JsonConvert.SerializeObject(e));
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FeatureFlagResponse>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByAllFeatureFlag()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.GetAllFeatureFlags();

                if (result == null || !result.Any())
                    return NoContent();

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                _logger.LogError("{error}", JsonConvert.SerializeObject(e));
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("{error}", JsonConvert.SerializeObject(e));
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FeatureFlagResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateFeatureFlag([FromBody] FeatureFlagCreate createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.CreateFeatureFlag(createDto);

                if (result == null)
                    return BadRequest();

                return Created(string.Empty, result);
            }
            catch (ArgumentException e)
            {
                _logger.LogError("{error}", JsonConvert.SerializeObject(e));
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("{error}", JsonConvert.SerializeObject(e));
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeatureFlagResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct([FromBody] FeatureFlagUpdate updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.UpdateFeatureFlag(updateDto);

                if (result == null)
                    return BadRequest();

                return Ok(result);
            }
            catch  (IntegrityException e)
            {
                return StatusCode((int)HttpStatusCode.NotFound, e.Message);
            }
            catch (ArgumentException e)
            {
                _logger.LogError("{error}", JsonConvert.SerializeObject(e));
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("{error}", JsonConvert.SerializeObject(e));
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}