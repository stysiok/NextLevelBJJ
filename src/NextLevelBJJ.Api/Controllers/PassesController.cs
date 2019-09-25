using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NextLevelBJJ.Application;
using NextLevelBJJ.Application.Passes.DTO;
using NextLevelBJJ.Application.Passes.Queries;

namespace NextLevelBJJ.Api.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class PassesController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public PassesController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PassDto>> Get([FromRoute]GetPass command)
        {
            var result = await _dispatcher.QueryAsync(command);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
