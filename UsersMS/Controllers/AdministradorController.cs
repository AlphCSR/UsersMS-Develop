using Microsoft.AspNetCore.Mvc;
using MediatR;
using UsersMS.Commons.Dtos.Request;
using UsersMS.Application.Commands;
using UsersMS.Application.Querys;
using Microsoft.AspNetCore.Authorization;

namespace UsersMS.Controllers
{
    [ApiController]
    [Authorize(Roles = "Administrador")]
    [Route("[controller]")]
    public class AdministradorController : ControllerBase
    {
        private readonly ILogger<AdministradorController> _logger;
        private readonly IMediator _mediator;

        public AdministradorController(ILogger<AdministradorController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger.LogInformation("AdministradorController instantiated");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdministrador(CreateAdministradorDto createAdministradorDto)
        {
            _logger.LogInformation("Received request to create an Administrador");
            try
            {
                var command = new CreateAdministradorCommand(createAdministradorDto);
                var message = await _mediator.Send(command);
                _logger.LogInformation("Successfully created Administrador: {Message}", message);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while creating an Administrador: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an Administrador.");
            }
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAdministradorById(DeleteAdministradorDto deleteAdministradorDto)
        {
            try
            {
                var command = new DeleteAdministradorCommand(deleteAdministradorDto);
                var message = await _mediator.Send(command);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to delete an Administrador {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an Administrador");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAdministrador(Guid Id)
        {
            try
            {
                var query = new GetAdministradorQuery(Id);
                var administrador = await _mediator.Send(query);
                return Ok(administrador);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting operators {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting operator.");
            }
        }


        [HttpPut]

        public async Task<IActionResult> UpdateAdministrador(UpdateAdministradorDto updateAdministradorDto)
        {
            try
            {
                var command = new UpdateAdministradorCommand(updateAdministradorDto);
                var msg = await _mediator.Send(command);
                return Ok(msg);


            }
            catch (Exception e)
            {

                _logger.LogError("An error occurred while trying to update an administrador {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an administrador");

            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdministradores()
        {
            try
            {
                var query = new GetAllAdministradoresQuery();
                var administradores = await _mediator.Send(query);
                return Ok(administradores);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting administradores {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting administradores.");
            }
        }
    }
}
