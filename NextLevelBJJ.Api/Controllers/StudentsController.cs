using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NextLevelBJJ.Application.Students.Commands;
using NextLevelBJJ.Application.Students.DTO;
using NextLevelBJJ.Application.Students.Services;

namespace NextLevelBJJ.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public StudentsController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        
        [HttpPost("sign-in")]
        public async Task<ActionResult<JwtDto>> SignIn(SignIn command)
        {
            var jwt = await _identityService.SignInAsync(command);

            return Ok(jwt);
        }
    }
}