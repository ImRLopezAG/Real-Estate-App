using Microsoft.AspNetCore.Mvc;
using Restaurant.Presentation.WebApi.Core;
using RSApp.Core.Application.Features.Properties.Queries.GetAll;
using RSApp.Core.Application.Features.Properties.Queries.GetByCode;
using RSApp.Core.Application.Features.Properties.Queries.GetById;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RSApp.Presentation.WebApi.Controllers {
    [ApiVersion("1.0")]
    [SwaggerTag("Maintenance of property types")]
    public class PropertyController : BaseApiController {
        [HttpGet("List")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            summary: "List of properties",
            description: "Get all properties"
        )]
        public async Task<IActionResult> List(GetAllPropertiesQuery query) {
            try {
                var result = await Mediator.Send(query);
                return Ok(result);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetById")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            summary: "Property by ID",
            description: "Get Property by ID"
        )]
        public async Task<IActionResult> GetById(GetByIdPropertiesQuery query) {
            try {
                var result = await Mediator.Send(query);
                return Ok(result);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Code")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            summary: "Property by Code",
            description: "Get Property by Code"
        )]
        public async Task<IActionResult> GetByCode(GetByCodePropertyQuery query) {
            try {
                var result = await Mediator.Send(query);
                return Ok(result);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}