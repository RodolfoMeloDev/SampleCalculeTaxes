using System.Net;
using CalculateTaxes.Data.Exception;
using CalculateTaxes.Domain.Dtos.Client;
using CalculateTaxes.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CalculateTaxes.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class ClientController(IClientService service, ILogger<ClientController> logger) : ControllerBase
    {
        private readonly IClientService _service = service;
        private readonly ILogger<ClientController> _logger = logger;

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponse))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdClient([FromQuery] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.GetByIdClient(id);

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
        [Route("CPF")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponse))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCPFClient([FromQuery] string cpf)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.GetByCPFClient(cpf);

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientResponse>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByAllClients()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.GetAllClients();

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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateClient([FromBody] ClientCreate createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.CreateClient(createDto);

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClient([FromBody] ClientUpdate updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.UpdateClient(updateDto);

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