using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using trailAPI.Models;

namespace trailAPI.Models
{
    public class Exploration
    {
        [Key]
        public int ExplorationID { get; set; }

        [Required]
        [ForeignKey("TrailInformation")]
        [Column("trailID")] // Map to the database column
        public string TrailID { get; set; }

        [Required]
        [Column("completionDate")] // Map to the database column
        public DateTime CompletionDate { get; set; }

        [Required]
        [Column("completionStatus")] // Map to the database column
        public bool CompletionStatus { get; set; }

        public int UserID { get; set; }

        public UserWithExploration UsersWithExploration { get; set; }

        public TrailInformation TrailInformation { get; set; }
    }
}