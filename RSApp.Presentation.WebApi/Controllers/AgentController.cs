using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Presentation.WebApi.Core;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RSApp.Presentation.WebApi.Controllers {
    [ApiVersion("1.0")]
    [SwaggerTag("Maintenance of user agent")]
    public class AgentController : BaseApiController {

        private readonly IAccountService _account;
        private readonly IPropertyService _service;


        public AgentController(IAccountService account, IPropertyService property) {
            _account = account;
            _service = property;
        }

        [HttpGet]
        [Authorize(Policy = "AdminOrDev")]
        [SwaggerOperation(
            summary: "List of agents",
            description: "Get all agents"
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List() {
            try {
                var props = await _service.GetAll();
                var users = await _account.GetAll().ContinueWith(r => r.Result.Join(props, u => u.Id, p => p.Agent, (u, p) => new {
                    u.Id,
                    u.FirstName,
                    u.LastName,
                    u.Email,
                    u.PhoneNumber,
                    u.Role,
                    Properties = props.Where(x => x.Agent == u.Id).Count()
                }).ToList());

                return Ok(users.Where(x => x.Role == "Agent"));
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrDev")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
       summary: "Get a agent by ID",
       description: "Get a agent by ID"
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string id) {
            try {
                var props = await _service.GetAll();
                var users = await _account.GetById(id);
                users.Products = props.Where(x => x.Agent == id).Count();
                return Ok(users);
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrDev")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
       summary: "Get property by agent ID",
       description: "Get property by agent ID"
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAgentProperty(string id) {
            try {
                var props = await _service.GetAll().ContinueWith(r => r.Result.Where(x => x.Agent == id).ToList());
                return Ok(props);
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }

        [HttpPost("{id}")]
        [Authorize(Policy = "Administrator")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
       summary: "Change by agent ID",
       description: "Change by agent ID"
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeStatus(string id) {
            try {
                var props = await _service.GetAll().ContinueWith(r => r.Result.Where(x => x.Agent == id).ToList());
                var userIsVerify = await _account.GetById(id);
                userIsVerify.Products = props.Count();
                await _account.ChangeStatus(userIsVerify.Id);

                return NoContent();
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
