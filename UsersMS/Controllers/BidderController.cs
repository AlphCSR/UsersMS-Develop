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
    public class BidderController : ControllerBase
    {
        private readonly ILogger<BidderController> _logger;
        private readonly IMediator _mediator;

        public BidderController (ILogger<BidderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBidder(CreateBidderDto createBidderDto)
        {
            try
            {
                var command = new CreateBidderCommand(createBidderDto);
                var message = await _mediator.Send(command);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError("A ocurrido un error mientras se creaba un Bidder {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an Bidder");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetBidder(Guid Id)
        {
            try
            {
                var query = new GetBidderQuery(Id);
                var bidder = await _mediator.Send(query);
                return Ok(bidder);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting bidder {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting bidder.");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteBidderById(Guid Id)
        {
            try
            {
                var _deleteBidderDto = new DeleteBidderDto
                {
                    BidderId = Id
                };
                var command = new DeleteBidderCommand(_deleteBidderDto);
                var message = await _mediator.Send(command);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to delete an Bidder {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an Bidder");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBidder(UpdateBidderDto updateBidderDto)
        {
            try
            {
                var command = new UpdateBidderCommand(updateBidderDto);
                var msg = await _mediator.Send(command);
                return Ok(msg);


            }
            catch (Exception e)
            {

                _logger.LogError("An error occurred while trying to update an Bidder {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an Bidder");

            }

        }


        [HttpGet]
        public async Task<IActionResult> GetAllBidderes()
        {
            try
            {
                var query = new GetAllTechnicalSupportsQuery();
                var bidderes = await _mediator.Send(query);
                return Ok(bidderes);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting bidderes {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting bidderes.");
            }
        }
    }
}
