using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextLevelBJJ.Application;
using NextLevelBJJ.Application.PassTypes.Commands;
using NextLevelBJJ.Application.PassTypes.DTO;
using NextLevelBJJ.Application.PassTypes.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NextLevelBJJ.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassTypesController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public PassTypesController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PassTypeDto>> Get([FromRoute]GetPassType command)
        {
            var result = await _dispatcher.QueryAsync(command);
            
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PassTypeDto>>> Get([FromQuery]GetPassTypes command)
        {
            var result = await _dispatcher.QueryAsync(command);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "Admin")]
        public async Task<IActionResult> Post(CreatePassType command)
        {
            await _dispatcher.SendAsync(command);

            return Created($"api/passtypes/{command.Id}", null);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _dispatcher.SendAsync(new DeletePassType { Id = id });

            return NoContent();
        }
    }
}
