using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trailAPI.Models
{
    [Table("Users")] 
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] // Map to the database column
        public int UserID { get; set; }

        [Required]
        [Column("firstName")] // Map to the database column
        public string FirstName { get; set; }

        [Required]
        [Column("lastName")] // Map to the database column
        public string LastName { get; set; }

        [Required]
        [Column("email")] // Map to the database column
        public string Email { get; set; }

        [Required]
        [Column("password")] // Map to the database column
        public string Password { get; set; }

        // Add the Explorations navigation property
        public ICollection<Exploration> Explorations { get; set; }
    }
}