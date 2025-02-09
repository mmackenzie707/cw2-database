using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trailAPI.Models
{
    [Table("Explorations")]
    public class Explorations
    {
        [Key]
        public int ExplorationID { get; set; }

        [Required]
        [Column("userID")]
        public int UserID { get; set; }

        [Required]
        [Column("trailID")]
        public int TrailID { get; set; }

        [Required]
        [Column("completionStatus")]
        public string CompletionStatus { get; set; }

        [Required]
        [Column("completionTime")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "CompletionTime must be exactly 10 characters long.")]
        public string CompletionTime { get; set; } = string.Empty;

        [Required]
        [Column("completionDate", TypeName = "datetime2")]
        public DateTime CompletionDate { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("TrailID")]
        public TrailInformation Trail { get; set; }
    }
}