using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trailAPI.Models
{
    [Table("Explorations")]
    public class Exploration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("explorationID")] // Map to the database column
        public int ExplorationID { get; set; }

        [Required]
        [ForeignKey("User")]
        [Column("userID")] // Map to the database column
        public int UserID { get; set; }

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

        public TrailInformation TrailInformation { get; set; }
    }
}