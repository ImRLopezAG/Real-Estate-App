using Microsoft.AspNetCore.Mvc;
using Restaurant.Presentation.WebApi.Core;
using RSApp.Core.Application.Features.Sales.Commands.Create;
using RSApp.Core.Application.Features.Sales.Commands.Delete;
using RSApp.Core.Application.Features.Sales.Commands.Update;
using RSApp.Core.Application.Features.Sales.Queries.GetAll;
using RSApp.Core.Application.Features.Sales.Queries.GetBbyId;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RSApp.Presentation.WebApi.Controllers {
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de ventas")]
    public class SaleController : BaseApiController {
        [HttpGet("List")]
        [SwaggerOperation(
            summary: "Listado de ventas",
            description: "Obtiene todas las ventas del sistema"
        )]
        public async Task<IActionResult> Get([FromQuery] GetAllSalesQuery query) {
            try {
                var result = await Mediator.Send(query);
                return Ok(result);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            summary: "Filtra las ventas por ID",
            description: "Obtiene una venta en especifico por el ID"
        )]
        public async Task<IActionResult> GetById([FromQuery] int id) {
            try {
                return Ok(await Mediator.Send(new GetByIdQuery { Id = id }));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            summary: "Creación de venta",
            description: "Recibe los parametros necesarios para crear una nueva venta"
        )]

        public async Task<IActionResult> Post([FromBody] CreateSaleCommand command) {
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
            summary: "Actualización de venta",
            description: "Recibe los parametros necesarios para actualizar una venta"
        )]

        public async Task<IActionResult> Put([FromBody] UpdateSaleCommand command) {
            try {
                return Ok(await Mediator.Send(command));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            summary: "Eliminar una venta",
            description: "Recibe un ID para eliminar la venta correspondiente"
        )]
        public async Task<IActionResult> Delete(int id) {
            try {
                await Mediator.Send(new DeleteSaleCommand { Id = id });
                return NoContent();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}