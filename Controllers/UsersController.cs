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

        // POST: api/Users/login
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Invalid login request");
            }

            var user = _userService.ValidateUser(new User { Email = loginRequest.Email, Password = loginRequest.Password });
            if (!user)
            {
                return Unauthorized();
            }

            var token = _tokenService.GenerateToken(new User { Email = loginRequest.Email });
            return Ok(new { Token = token });
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

        // POST: api/Users/{userId}/explorations
        [Authorize]
        [HttpPost("{userId}/explorations")]
        public IActionResult AddExploration(int userId, [FromBody] Exploration exploration)
        {
            if (exploration == null || exploration.UserID != userId)
            {
                return BadRequest("Exploration data is invalid");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _userService.AddExploration(exploration);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { Status = "Exploration added", Exploration = exploration });
        }

        // GET: api/Users/{userId}/explorations
        [Authorize]
        [HttpGet("{userId}/explorations")]
        public IActionResult GetExplorations(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }

            var explorations = _userService.GetExplorationsByUserId(userId)
                .Select(e => new UserExplorationResponseDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    TrailName = e.TrailInformation.trailName,
                    TrailLocation = e.TrailInformation.trailLocation,
                    CompletionDate = e.CompletionDate,
                    CompletionStatus = e.CompletionStatus
                }).ToList();

            return Ok(explorations);
        }

        // POST: api/Users/with-exploration
        [Authorize]
        [HttpPost("with-exploration")]
        public IActionResult AddUserWithExploration([FromBody] UserExplorationDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Data is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _userService.AddUserWithExploration(dto);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { Status = "User and Exploration added", Data = dto });
        }

        // New endpoints for user rights

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

                // GET: api/Users/notice
        [AllowAnonymous]
        [HttpGet("notice")]
        public IActionResult GetNotice()
        {
            var noticePath = Path.Combine(Directory.GetCurrentDirectory(), "notice.txt");
            if (!System.IO.File.Exists(noticePath))
            {
                return NotFound("Notice not found.");
            }

            var notice = System.IO.File.ReadAllText(noticePath);
            return Ok(new { Notice = notice });
        }
    }
}