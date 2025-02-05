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
        [Column("username")] // Map to the database column
        public string Email { get; set; }

        [Required]
        [Column("password")] // Map to the database column
        public string Password { get; set; }
    }
}