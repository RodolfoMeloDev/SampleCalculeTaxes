using System.Net;
using CalculateTaxes.Domain.Dtos.Order;
using CalculateTaxes.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CalculateTaxes.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class OrderController(IOrderService service, ILogger<OrderController> logger) : ControllerBase
    {
        private readonly IOrderService _service = service;
        private readonly ILogger<OrderController> _logger = logger;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderCreateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreate createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.CreateOrder(createDto);

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
        [Route("RecalculateTaxes/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RecalculateTaxes(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.RecalculateTax(id);

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
        [Route("id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderResponse))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdOrder([FromQuery] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.GetByIdOrder(id);

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
        [Route("pedidos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderResponse>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByStatusOrder([FromQuery] string status)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.GetByStatusOrder(status);

                if (!result.Any())
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
    }
}