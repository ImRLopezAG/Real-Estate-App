using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Presentation.WebApi.Core;
using RSApp.Core.Application.Features.Upgrades.Commands.Create;
using RSApp.Core.Application.Features.Upgrades.Commands.Delete;
using RSApp.Core.Application.Features.Upgrades.Commands.Update;
using RSApp.Core.Application.Features.Upgrades.Queries.GetAll;
using RSApp.Core.Application.Features.Upgrades.Queries.GetBbyId;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Presentation.WebApi.Controllers
{
  [ApiVersion("1.0")]
  [SwaggerTag("Maintenance of upgrades")]
  public class UpgradeController : BaseApiController
  {
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        summary: "List of upgrades",
        description: "Get all upgrades"
    )]
    [Authorize(Policy = "AdminOrDev")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> List(GetAllUpgradeQuery query)
    {
      try
      {
        var result = await Mediator.Send(query);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status404NotFound, ex.Message);
      }
    }

    [HttpGet("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        summary: "Get a upgrade by ID",
        description: "Get a upgrade by ID"
    )]
    [Authorize(Policy = "AdminOrDev")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] int id)
    {
      try
      {
        return Ok(await Mediator.Send(new GetByIdUpgradeQuery { Id = id }));
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status404NotFound, ex.Message);
      }
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        summary: "Create a new upgrade",
        description: "Get the parameters to create a new upgrade"
    )]
    [Authorize(Policy = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] CreateUpgradeCommand command)
    {
      try
      {
        await Mediator.Send(command);
        return NoContent();
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status404NotFound, ex.Message);
      }
    }

    [HttpPut("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        summary: "Update a upgrade",
        description: "Get the parameters to update a upgrade"
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Policy = "Administrator")]
    public async Task<IActionResult> Put([FromBody] UpdateUpgradeCommand command)
    {
      try
      {
        var result = await Mediator.Send(command);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status404NotFound, ex.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
      try
      {
        await Mediator.Send(new DeleteUpgradeCommand { Id = id });
        return NoContent();
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
      }
    }

  }
}