using System;
using System.ComponentModel.DataAnnotations;

namespace trailAPI.Controllers
{
    public class ExplorationDto
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public int TrailID { get; set; }

        [Required]
        public string CompletionStatus { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "CompletionTime must be exactly 10 characters long.")]
        public string CompletionTime { get; set; }
        [Required]
        public DateTime CompletionDate { get; set; }
    }
}