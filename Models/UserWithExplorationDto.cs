using System.ComponentModel.DataAnnotations;
using trailAPI.Models;

public class UserWithExplorationDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public ICollection<ExplorationDto> Explorations { get; set; }
    }