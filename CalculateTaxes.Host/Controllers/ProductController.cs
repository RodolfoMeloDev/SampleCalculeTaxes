using System.Net;
using CalculateTaxes.Data.Exception;
using CalculateTaxes.Domain.Dtos.Product;
using CalculateTaxes.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CalculateTaxes.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductController(IProductService service, ILogger<ProductController> logger) : ControllerBase
    {
        private readonly ILogger<ProductController> _logger = logger;
        private readonly IProductService _service = service;

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdProduct([FromQuery] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.GetByIdProduct(id);

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductResponse>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByAllProduct()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.GetAllProducts();

                if (result == null || result.Count() == 0)
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreate createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.CreateProduct(createDto);

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdate updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.UpdateProduct(updateDto);

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