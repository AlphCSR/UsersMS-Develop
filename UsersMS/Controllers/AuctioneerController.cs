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
    [Authorize(Roles = "Administrador")]
    [Route("[controller]")]
    public class AuctioneerController : ControllerBase
    {
        private readonly ILogger<AuctioneerController> _logger;
        private readonly IMediator _mediator;

        public AuctioneerController(ILogger<AuctioneerController> logger, IMediator mediator) { 
            _logger = logger;
            _mediator = mediator;   
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuctioneer(CreateAuctioneerDto createAuctioneerDto)
        {
            try
            {
                var command = new CreateAuctioneerCommand(createAuctioneerDto);
                var message = await _mediator.Send(command);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError("A ocurrido un error mientras se creaba un Auctioneer {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an Auctioneer");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAuctioneer(Guid Id)
        {
            try
            {
                var query = new GetTechnicalSupportQuery(Id);
                var auctioneer = await _mediator.Send(query);
                return Ok(auctioneer);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting Auctioneer {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting Auctioneer.");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAuctioneerById(Guid Id)
        {
            try
            {
                var _deleteAuctioneerDto = new DeleteAuctioneerDto
                {
                    AuctioneerId = Id
                };
                var command = new DeleteAuctioneerCommand(_deleteAuctioneerDto);
                var message = await _mediator.Send(command);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to delete an Auctioneer {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an Auctioneer");
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAuctioneer(UpdateAuctioneerDto updateAuctioneerDto)
        {
            try
            {
                var command = new UpdateAuctioneerCommand(updateAuctioneerDto);
                var msg = await _mediator.Send(command);
                return Ok(msg);


            }
            catch (Exception e)
            {

                _logger.LogError("An error occurred while trying to update an Auctioneer {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an Auctioneer");

            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuctioneeres()
        {
            try
            {
                var query = new GetAllBiddersQuery();
                var Auctioneeres = await _mediator.Send(query);
                return Ok(Auctioneeres);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting Auctioneeres {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting Auctioneeres.");
            }
        }
    }
}
