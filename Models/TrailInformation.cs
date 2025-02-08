using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trailAPI.Models
{
    [Table("Trail_Information")] // Ensure this matches your database table name
    public class TrailInformation
    {
        [Key]
        [Column("trailID")] // Map to the database column
        public string TrailID { get; set; }

        // Add other properties as needed
        public string trailName { get; set; }
        public string trailDescription { get; set; }
        public string trailLocation { get; set; }
    }
}