using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trailAPI.Models
{
    [Table("Explorations")] // Ensure this matches your database table name
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
        [Column("trailID")] // Map to the database column
        public string TrailID { get; set; }

        [Required]
        [Column("completionDate")] // Map to the database column
        public DateTime CompletionDate { get; set; }

        [Required]
        [Column("completionStatus")] // Map to the database column
        public bool CompletionStatus { get; set; }

        public User User { get; set; }
    }
}