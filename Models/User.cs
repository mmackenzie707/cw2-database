using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace trailAPI.Models
{
    [Table("UserView")]
    public class User
    {
        [Key]
        [Column("User")]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Valid { get; set; }
    }
}