using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { set; get; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}