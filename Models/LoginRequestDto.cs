using System.ComponentModel.DataAnnotations;

namespace trailAPI.Models
{
    public class LoginRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}