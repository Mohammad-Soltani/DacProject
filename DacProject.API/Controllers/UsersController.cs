using DacProject.Application.Interfaces.Services;
using DacProject.Application.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DacProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UsersController>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator , Standard")]
        [HttpGet]
        public async Task<ActionResult<List<UserViewModel>>> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            if (result is not null)
                return Ok(result);
            else return NotFound();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator , Standard")]
        public async Task<ActionResult<UserViewModel>> GetUserById(int id)
        {
            var result = await _userService.GetUserById(id);
            if (result is not null)
                return Ok(result);
            else return NotFound();
        }

        // GET api/<UsersController>/5
        [HttpGet]
        [Route("GetUserByEmail")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator , Standard")]
        public async Task<ActionResult<UserViewModel>> GetUserByEmail([FromQuery] string Email)
        {
            var result = await _userService.GetUserByEmail(Email);
            if (result is not null)
                return Ok(result);
            else return NotFound();
        }

        // POST api/<UsersController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<bool>> AddNewUser([FromBody] UserViewModel model)
        {
            var result = await _userService.AddOrUpdateUser(model);
            if (result)
                return Ok(result);
            else return NotFound();
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<bool>> UpdateUser([FromBody] UserViewModel model)
        {
            var result = await _userService.AddOrUpdateUser(model);
            if (result)
                return Ok(result);
            else return NotFound();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result)
                return Ok(result);
            else return NotFound();
        }
    }
}
