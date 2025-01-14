using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;



namespace AuthenticatorDEV.Models
    {
            [Table("UserView")]
public class User
        {
            [Key]
            [Column("User")]
        // You should add the other fields.
                public string Email { get; set; } = string.Empty;
                public string Password { get; set; } = string.Empty;
                public bool Valid { get; set;} = false;
        
        }
    }