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
    public class ProveedorController : ControllerBase
    {
        private readonly ILogger<ProveedorController> _logger;
        private readonly IMediator _mediator;

        public ProveedorController(ILogger<ProveedorController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProveedor(CreateProveedorDto createProveedorDto)
        {
            try
            {
                var command = new CreateProveedorCommand(createProveedorDto);
                var message = await _mediator.Send(command);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError("A ocurrido un error mientras se creaba un Proveedor {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to create an Proveedor");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProveedor(Guid Id)
        {
            try
            {
                var query = new GetProveedorQuery(Id);
                var proveedor = await _mediator.Send(query);
                return Ok(proveedor);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting Proveedor {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting Proveedor.");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProveedorById(Guid Id)
        {
            try
            {
                var _deleteProveedorDto = new DeleteProveedorDto
                {
                    ProveedorId = Id
                };
                var command = new DeleteProveedorCommand(_deleteProveedorDto);
                var message = await _mediator.Send(command);
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while trying to delete an Proveedor {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to delete an Proveedor");
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProveedor(UpdateProveedorDto updateProveedorDto)
        {
            try
            {
                var command = new UpdateProveedorCommand(updateProveedorDto);
                var msg = await _mediator.Send(command);
                return Ok(msg);


            }
            catch (Exception e)
            {

                _logger.LogError("An error occurred while trying to update an Proveedor {Message}", e.Message);
                return StatusCode(500, "An error occurred while trying to update an Proveedor");

            }



        }

        [HttpGet]
        public async Task<IActionResult> GetAllProveedores()
        {
            try
            {
                var query = new GetAllProveedoresQuery();
                var Proveedores = await _mediator.Send(query);
                return Ok(Proveedores);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting Proveedores {Message}", e.Message);
                return StatusCode(500, "An error occurred while getting Proveedores.");
            }
        }

    }
}
