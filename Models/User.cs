using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trailAPI.Models
{
    [Table("Users")] // Ensure this matches your database table name
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] // Map to the database column
        public int UserID { get; set; }

        [Required]
        [EmailAddress]
        [Column("username")] // Map to the database column
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [Column("password")] // Map to the database column
        public string Password { get; set; }

        [Required]
        [Column("first_name")] // Map to the database column
        public string FirstName { get; set; }

        [Required]
        [Column("last_name")] // Map to the database column
        public string LastName { get; set; }
    }
}