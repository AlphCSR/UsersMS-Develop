using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UsersMS.Application.Commands;
using UsersMS.Application.Querys;
using UsersMS.Commons.Dtos.Request;
using UsersMS.Domain.Entities;


namespace UsersMS.Controllers
{

    [ApiController]
    [Authorize(Roles = "Administrador, Proveedor")]
    [Route("[controller]")]
    public class ConductorController : ControllerBase
    {
        private readonly ILogger<ConductorController> _logger;
        private readonly IMediator _mediator;

        public ConductorController(ILogger<ConductorController> logger, IMediator mediator) { 
            _logger = logger;
            _mediator = mediator;   
        }

        [HttpPost]
        public async Task<IActionResult> CreateConductor(CreateConductorDto createConductorDto)
        {
            try
            {
                var command = new CreateConductorCommand(createConductorDto);
                var message = await _mediator.Send(command);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError("A ocurrido un error mientras se creaba un Conductor {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an Conductor");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetConductor(Guid Id)
        {
            try
            {
                var query = new GetConductorQuery(Id);
                var conductor = await _mediator.Send(query);
                return Ok(conductor);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting Conductor {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting Conductor.");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteConductorById(Guid Id)
        {
            try
            {
                var _deleteConductorDto = new DeleteConductorDto
                {
                    ConductorId = Id
                };
                var command = new DeleteConductorCommand(_deleteConductorDto);
                var message = await _mediator.Send(command);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to delete an Conductor {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an Conductor");
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateConductor(UpdateConductorDto updateConductorDto)
        {
            try
            {
                var command = new UpdateConductorCommand(updateConductorDto);
                var msg = await _mediator.Send(command);
                return Ok(msg);


            }
            catch (Exception e)
            {

                _logger.LogError("An error occurred while trying to update an Conductor {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an Conductor");

            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllConductores()
        {
            try
            {
                var query = new GetAllConductoresQuery();
                var Conductores = await _mediator.Send(query);
                return Ok(Conductores);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting Conductores {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting Conductores.");
            }
        }
    }
}
