using System.ComponentModel.DataAnnotations;

namespace Article.MVC.Models
{
    public class UserSignInModel
    {
        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is requried!")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
