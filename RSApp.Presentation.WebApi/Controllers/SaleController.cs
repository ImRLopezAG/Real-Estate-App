using Microsoft.AspNetCore.Authorization;
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
  [SwaggerTag("Maintenance of sales")]
  public class SaleController : BaseApiController {
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        summary: "List of sales",
        description: "Get all sales"
    )]
    [Authorize(Policy = "AdminOrDev")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> List(GetAllSalesQuery query) {
      try {
        var result = await Mediator.Send(query);
        return Ok(result);
      } catch (Exception ex) {
        return StatusCode(StatusCodes.Status404NotFound, ex.Message);
      }
    }

    [HttpGet("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        summary: "Get a sale by ID",
        description: "Get a sale by ID"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Policy = "AdminOrDev")]
    public async Task<IActionResult> Get([FromQuery] int id) {
      try {
        return Ok(await Mediator.Send(new GetByIdQuery { Id = id }));
      } catch (Exception ex) {
        return StatusCode(StatusCodes.Status404NotFound, ex.Message);
      }
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        summary: "Create a new sale",
        description: "Get the parameters to create a new sale"
    )]
    [Authorize(Policy = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> Create([FromBody] CreateSaleCommand command) {
      try {
        await Mediator.Send(command);
        return NoContent();
      } catch (Exception ex) {
        return StatusCode(StatusCodes.Status404NotFound, ex.Message);
      }
    }

    [HttpPut("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        summary: "Update a sale",
        description: "Get the parameters to update a sale"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Policy = "Administrator")]
    public async Task<IActionResult> Update([FromBody] UpdateSaleCommand command) {
      try {
        return Ok(await Mediator.Send(command));
      } catch (Exception ex) {
        return StatusCode(StatusCodes.Status404NotFound, ex.Message);
      }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        summary: "Delete a sale",
        description: "Get the ID of the sale to delete"
    )]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Policy = "Administrator")]
    public async Task<IActionResult> Delete(int id) {
      try {
        await Mediator.Send(new DeleteSaleCommand { Id = id });
        return NoContent();
      } catch (Exception ex) {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
      }
    }
  }
}