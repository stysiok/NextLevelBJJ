using Microsoft.AspNetCore.Mvc;
using NextLevelBJJ.Application;
using NextLevelBJJ.Application.PassTypes.Commands;
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

        [HttpPost]
        public async Task<IActionResult> Post(CreatePassType command)
        {
            await _dispatcher.SendAsync(command);

            return Created($"api/passtype/{command.Id}", null);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var result = await _dispatcher.QueryAsync<>
            return Ok();
        }
    }
}
