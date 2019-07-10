using Microsoft.AspNetCore.Mvc;
using NextLevelBJJ.Application;
using NextLevelBJJ.Application.PassTypes.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextLevelBJJ.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassTypeController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public PassTypeController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreatePassType command)
        {
            await _dispatcher.SendAsync(command);

            return Created($"api/passtypes/{command.Id}", null);
        }
    }
}
