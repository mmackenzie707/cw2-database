using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trailAPI.Models;
using trailAPI.Services;

namespace trailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserServices _userService;
        private readonly TokenService _tokenService;

        public UsersController(UserServices userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("Invalid login request");
            }

            var user = _userService.ValidateUser(loginDto.Username, loginDto.Password);
            if (user == null)
            {
                return Unauthorized(new { Status = "Login failed", Error = "Invalid username or password" });
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Status = "Login successful", Token = token });
        }

        // GET: api/Users
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }

        // GET: api/Users/{id}
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/Users
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] User usr)
        {
            if (usr == null)
            {
                return BadRequest("User data is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userService.AddUser(usr);
            return Ok(new { Status = "User added", User = usr });
        }

        // PUT: api/Users/{id}
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User usr)
        {
            if (usr == null || usr.UserID != id)
            {
                return BadRequest("User data is invalid");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = _userService.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            _userService.UpdateUser(usr);
            return Ok(new { Status = "User updated", User = usr });
        }

        // DELETE: api/Users/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            _userService.DeleteUser(id);
            return Ok(new { Status = "User deleted" });
        }

        // GET: api/Users/email/{email}
        [Authorize]
        [HttpGet("email/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            var user = _userService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // PUT: api/Users/email
        [Authorize]
        [HttpPut("email")]
        public IActionResult UpdateUserEmail([FromBody] UpdateEmailDto updateEmailDto)
        {
            if (updateEmailDto == null || string.IsNullOrEmpty(updateEmailDto.OldEmail) || string.IsNullOrEmpty(updateEmailDto.NewEmail))
            {
                return BadRequest("Invalid email update request");
            }

            _userService.UpdateUserEmail(updateEmailDto.OldEmail, updateEmailDto.NewEmail);
            return Ok(new { Status = "Email updated" });
        }

        // DELETE: api/Users/email/{email}
        [Authorize]
        [HttpDelete("email/{email}")]
        public IActionResult DeleteUserByEmail(string email)
        {
            _userService.DeleteUserByEmail(email);
            return Ok(new { Status = "User deleted" });
        }

        // GET: api/Users/privacy-policy
        [AllowAnonymous]
        [HttpGet("privacy-policy")]
        public IActionResult GetPrivacyPolicy()
        {
            var privacyPolicyPath = Path.Combine(Directory.GetCurrentDirectory(), "privacy-policy.txt");
            if (!System.IO.File.Exists(privacyPolicyPath))
            {
                return NotFound("Privacy policy not found.");
            }

            var privacyPolicy = System.IO.File.ReadAllText(privacyPolicyPath);
            return Ok(new { Policy = privacyPolicy });
        }

        // POST: api/Users/with-explorations
        [Authorize]
        [HttpPost("with-explorations")]
        public IActionResult PostWithExplorations([FromBody] UserWithExploration usr)
        {
            if (usr == null)
            {
                return BadRequest("User data is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var explorationDtos = usr.Explorations.Select(e => new ExplorationDto
            {
                // Map properties from Exploration to ExplorationDto
                TrailID = e.TrailID,
                CompletionDate = e.CompletionDate,
                CompletionStatus = e.CompletionStatus
            });

            _userService.AddUserWithExploration(usr, explorationDtos);
            return Ok(new { Status = "User with explorations added", User = usr });
        }
    }
}