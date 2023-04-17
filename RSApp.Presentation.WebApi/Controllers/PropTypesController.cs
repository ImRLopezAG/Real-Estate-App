using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Presentation.WebApi.Core;
using Microsoft.AspNetCore.Mvc;
using RSApp.Core.Application.Features.PropTypes.Queries.GetAll;
using RSApp.Core.Application.Features.PropTypes.Queries.GetBbyId;
using RSApp.Core.Application.Features.PropTypes.Commands.Create;
using RSApp.Core.Application.Features.PropTypes.Commands.Update;
using RSApp.Core.Application.Features.PropTypes.Commands.Delete;

namespace RSApp.Presentation.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class PropTypesController : BaseApiController
    {
        [HttpGet("List")]
        public async Task<IActionResult> Get([FromQuery] GetAllPropTypesQuery query)
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
                return Ok(await Mediator.Send(new GetByIdPropTypeQuery { Id = id }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] CreatePropTypeCommand command)
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
        public async Task<IActionResult> Put([FromQuery] UpdatePropTypeCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeletePropTypeCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}