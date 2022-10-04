using DacProject.Application.Interfaces.Services;
using DacProject.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DacProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly IUserAuthenticationService _userAuthenticationService;
        public LoginsController(IUserAuthenticationService userAuthenticationService)
        {
            _userAuthenticationService = userAuthenticationService;
        }

        [AllowAnonymous]
        [Route("Authenticate")]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginViewModel model)
        {
            var token = await _userAuthenticationService.Authenticate(model);
            if (string.IsNullOrEmpty(token))
                return Unauthorized();
            return Ok(token);
        }
    }
}
