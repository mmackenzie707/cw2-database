using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trailAPI.Models
{
    [Table("IsAuthorized")]
    public class IsAuthorized
    {
        [Key]
        public int AuthorizationID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public bool IsAuthorizedUser { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}