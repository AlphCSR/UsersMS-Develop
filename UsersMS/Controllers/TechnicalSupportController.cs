using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UsersMS.Application.Commands;
using UsersMS.Application.Querys;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Controllers
{
    [ApiController]
    [Authorize(Roles = "Administrador")]
    [Route("[controller]")]
    public class TechnicalSupportController : ControllerBase
    {
        private readonly ILogger<TechnicalSupportController> _logger;
        private readonly IMediator _mediator;

        public TechnicalSupportController(ILogger<TechnicalSupportController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTechnicalSupport(CreateTechnicalSupportDto createTechnicalSupportDto)
        {
            try
            {
                var command = new CreateTechnicalSupportCommand(createTechnicalSupportDto);
                var message = await _mediator.Send(command);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError("A ocurrido un error mientras se creaba un TechnicalSupport {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an TechnicalSupport");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetTechnicalSupport(Guid Id)
        {
            try
            {
                var query = new GetAuctioneerQuery(Id);
                var technicalSupport = await _mediator.Send(query);
                return Ok(technicalSupport);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting TechnicalSupport {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting TechnicalSupport.");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTechnicalSupportById(Guid Id)
        {
            try
            {
                var _deleteTechnicalSupportDto = new DeleteTechnicalSupportDto
                {
                    TechnicalSupportId = Id
                };
                var command = new DeleteTechnicalSupportCommand(_deleteTechnicalSupportDto);
                var message = await _mediator.Send(command);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to delete an TechnicalSupport {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an TechnicalSupport");
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateTechnicalSupport(UpdateTechnicalSupportDto updateTechnicalSupportDto)
        {
            try
            {
                var command = new UpdateTechnicalSupportCommand(updateTechnicalSupportDto);
                var msg = await _mediator.Send(command);
                return Ok(msg);


            }
            catch (Exception e)
            {

                _logger.LogError("An error occurred while trying to update an TechnicalSupport {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an TechnicalSupport");

            }



        }

        [HttpGet]
        public async Task<IActionResult> GetAllTechnicalSupportes()
        {
            try
            {
                var query = new GetAllAuctioneersQuery();
                var TechnicalSupportes = await _mediator.Send(query);
                return Ok(TechnicalSupportes);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting TechnicalSupportes {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting TechnicalSupportes.");
            }
        }

    }
}
