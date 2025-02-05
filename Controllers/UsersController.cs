using Microsoft.AspNetCore.Mvc;
using trailAPI.Models;
using trailAPI.Services;

namespace trailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult Post([FromBody] User usr)
        {
            if (usr == null)
            {
                return BadRequest("User data is null");
            }

            bool Verified = _userService.ValidateUser(usr);
            return Ok(new { Status = "Verified", IsVerified = Verified });
        }
    }
}