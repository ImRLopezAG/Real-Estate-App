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
  [SwaggerTag("Maintenance of property types")]
  public class PropTypesController : BaseApiController {
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        summary: "List of property types",
        description: "Get all property types"
    )]
    public async Task<IActionResult> List(GetAllPropTypesQuery query) {
      try {
        var result = await Mediator.Send(query);
        return Ok(result);
      } catch (Exception ex) {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        summary: "Get a property type by ID",
        description: "Get a property type by ID"
    )]
    public async Task<IActionResult> Get([FromQuery] int id) {
      try {
        return Ok(await Mediator.Send(new GetByIdPropTypeQuery { Id = id }));
      } catch (Exception ex) {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        summary: "Create a new property type",
        description: "Get the parameters to create a new property type"
    )]

    public async Task<IActionResult> Create([FromBody] CreatePropTypeCommand command) {
      try {
        await Mediator.Send(command);
        return NoContent();
      } catch (Exception ex) {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        summary: "Update a property type",
        description: "Get the parameters to update a property type"
    )]

    public async Task<IActionResult> Update([FromBody] UpdatePropTypeCommand command) {
      try {
        return Ok(await Mediator.Send(command));
      } catch (Exception ex) {
        return BadRequest(ex.Message);
      }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        summary: "Delete a property type",
        description: "Get the ID of the property type to delete"
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