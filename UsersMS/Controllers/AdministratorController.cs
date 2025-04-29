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
    public class AdministratorController : ControllerBase
    {
        private readonly ILogger<AdministratorController> _logger;
        private readonly IMediator _mediator;

        public AdministratorController(ILogger<AdministratorController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger.LogInformation("AdministratorController instantiated");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdministrator(CreateAdministratorDto createAdministratorDto)
        {
            _logger.LogInformation("Received request to create an Administrator");
            try
            {
                var command = new CreateAdministratorCommand(createAdministratorDto);
                var message = await _mediator.Send(command);
                _logger.LogInformation("Successfully created Administrator: {Message}", message);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while creating an Administrator: {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an Administrator.");
            }
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAdministratorById(DeleteAdministratorDto deleteAdministratorDto)
        {
            try
            {
                var command = new DeleteAdministratorCommand(deleteAdministratorDto);
                var message = await _mediator.Send(command);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to delete an Administrator {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an Administrator");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAdministrator(Guid Id)
        {
            try
            {
                var query = new GetAdministratorQuery(Id);
                var administrator = await _mediator.Send(query);
                return Ok(administrator);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting operators {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting operator.");
            }
        }


        [HttpPut]

        public async Task<IActionResult> UpdateAdministrator(UpdateAdministratorDto updateAdministratorDto)
        {
            try
            {
                var command = new UpdateAdministratorCommand(updateAdministratorDto);
                var msg = await _mediator.Send(command);
                return Ok(msg);


            }
            catch (Exception e)
            {

                _logger.LogError("An error occurred while trying to update an administrator {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an administrator");

            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdministratores()
        {
            try
            {
                var query = new GetAllAdministratorsQuery();
                var administratores = await _mediator.Send(query);
                return Ok(administratores);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting administratores {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting administratores.");
            }
        }
    }
}
