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
    public class OperadorController : ControllerBase
    {
        private readonly ILogger<OperadorController> _logger;
        private readonly IMediator _mediator;

        public OperadorController (ILogger<OperadorController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOperador(CreateOperadorDto createOperadorDto)
        {
            try
            {
                var command = new CreateOperadorCommand(createOperadorDto);
                var message = await _mediator.Send(command);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError("A ocurrido un error mientras se creaba un Operador {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an Operador");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOperador(Guid Id)
        {
            try
            {
                var query = new GetOperadorQuery(Id);
                var operador = await _mediator.Send(query);
                return Ok(operador);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting operador {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting operador.");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteOperadorById(Guid Id)
        {
            try
            {
                var _deleteOperadorDto = new DeleteOperadorDto
                {
                    OperadorId = Id
                };
                var command = new DeleteOperadorCommand(_deleteOperadorDto);
                var message = await _mediator.Send(command);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to delete an Operador {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an Operador");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOperador(UpdateOperadorDto updateOperadorDto)
        {
            try
            {
                var command = new UpdateOperadorCommand(updateOperadorDto);
                var msg = await _mediator.Send(command);
                return Ok(msg);


            }
            catch (Exception e)
            {

                _logger.LogError("An error occurred while trying to update an Operador {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an Operador");

            }

        }


        [HttpGet]
        public async Task<IActionResult> GetAllOperadores()
        {
            try
            {
                var query = new GetAllOperadoresQuery();
                var operadores = await _mediator.Send(query);
                return Ok(operadores);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting operadores {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting operadores.");
            }
        }
    }
}
