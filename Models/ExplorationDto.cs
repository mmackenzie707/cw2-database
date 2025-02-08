using System.ComponentModel.DataAnnotations;

public class ExplorationDto
    {
        [Required]
        public string TrailID { get; set; }

        [Required]
        public DateTime CompletionDate { get; set; }

        [Required]
        public bool CompletionStatus { get; set; }
    }