using AsyncInn.Interfaces;
using AsyncInn.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AsyncInn.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUser data)
        {
            var res = await _userService.Register(data,this.ModelState);
            if (ModelState.IsValid)
            {
                return Ok(res);
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("Login")]
        public async Task <ActionResult<UserDto>> Authenticate (string username, string password)
        {
            var res = await _userService.Authenticate(username, password);
            if (res != null)
            {
                return Ok(res);
            }

            return Unauthorized();
        }
        //Allowed only for the authorized 
        //[Authorize]
        [Authorize (Roles ="Manager")]

        [HttpGet("Me")]
        public async Task<ActionResult<UserDto>> Me()
        {
            return await _userService.GetUser(this.User);
        }

    }
}
