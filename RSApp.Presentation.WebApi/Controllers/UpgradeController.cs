using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Presentation.WebApi.Core;
using RSApp.Core.Application.Features.Upgrades.Commands.Create;
using RSApp.Core.Application.Features.Upgrades.Commands.Delete;
using RSApp.Core.Application.Features.Upgrades.Commands.Update;
using RSApp.Core.Application.Features.Upgrades.Queries.GetAll;
using RSApp.Core.Application.Features.Upgrades.Queries.GetBbyId;

namespace RSApp.Presentation.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class UpgradeController : BaseApiController
    {
        [HttpGet("List")]
        public async Task<IActionResult> Get([FromQuery] GetAllUpgradeQuery query)
        {
            try
            {
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetByIdUpgradeQuery { Id = id }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] CreateUpgradeCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] UpdateUpgradeCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                await Mediator.Send(new DeleteUpgradeCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}