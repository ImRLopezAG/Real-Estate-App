using Microsoft.AspNetCore.Mvc;
using Restaurant.Presentation.WebApi.Core;
using RSApp.Core.Application.Features.PropTypes.Commands.Create;
using RSApp.Core.Application.Features.PropTypes.Commands.Delete;
using RSApp.Core.Application.Features.PropTypes.Commands.Update;
using RSApp.Core.Application.Features.PropTypes.Queries.GetAll;
using RSApp.Core.Application.Features.PropTypes.Queries.GetBbyId;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RSApp.Presentation.WebApi.Controllers {
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de tipo de propiedades")]
    public class PropTypesController : BaseApiController {
        [HttpGet("List")]
        [SwaggerOperation(
            summary: "Listado de tipos de propiedades",
            description: "Obtiene todas las ventas del sistema"
        )]
        public async Task<IActionResult> Get([FromQuery] GetAllPropTypesQuery query) {
            try {
                var result = await Mediator.Send(query);
                return Ok(result);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            summary: "Filtra los tipos de propiedades por ID",
            description: "Obtiene el tipo de propiedad por ID"
        )]
        public async Task<IActionResult> GetById([FromQuery] int id) {
            try {
                return Ok(await Mediator.Send(new GetByIdPropTypeQuery { Id = id }));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            summary: "Creaci�n de tipos de propiedades",
            description: "Recibe los parametros necesarios para crear tipos de propiedades"
        )]

        public async Task<IActionResult> Post([FromQuery] CreatePropTypeCommand command) {
            try {
                await Mediator.Send(command);
                return NoContent();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            summary: "Actualizaci�n de tipos de propiedades",
            description: "Recibe los parametros necesarios para actualizar tipos de propiedades"
        )]

        public async Task<IActionResult> Put([FromQuery] UpdatePropTypeCommand command) {
            try {
                return Ok(await Mediator.Send(command));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            summary: "Eliminar un tipo de propiedad",
            description: "Recibe un ID para eliminar un tipo de propiedad"
        )]
        public async Task<IActionResult> Delete(int id) {
            try {
                await Mediator.Send(new DeletePropTypeCommand { Id = id });
                return NoContent();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}