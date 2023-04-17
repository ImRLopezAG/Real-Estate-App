using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Presentation.WebApi.Core;
using RSApp.Core.Application.Features.Sales.Commands.Create;
using RSApp.Core.Application.Features.Sales.Commands.Delete;
using RSApp.Core.Application.Features.Sales.Commands.Update;
using RSApp.Core.Application.Features.Sales.Queries.GetAll;
using RSApp.Core.Application.Features.Sales.Queries.GetBbyId;

namespace RSApp.Presentation.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class SaleController : BaseApiController
    {
        [HttpGet("List")]
        public async Task<IActionResult> Get([FromQuery] GetAllSalesQuery query)
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
                return Ok(await Mediator.Send(new GetByIdQuery { Id = id }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] CreateSaleCommand command)
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
        public async Task<IActionResult> Put([FromQuery] UpdateSaleCommand command)
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
                await Mediator.Send(new DeleteSaleCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}