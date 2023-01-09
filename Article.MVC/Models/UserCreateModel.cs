using System.ComponentModel.DataAnnotations;

namespace Article.MVC.Models
{
    public class UserCreateModel
    {
        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }
        [EmailAddress(ErrorMessage = "Please enter proper email adress!")]
        [Required(ErrorMessage = "Email adress is required!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords are not matched!")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "InviteCode is required!")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "The field must be a 5-digit number.")]
        [Range(10000, 99999, ErrorMessage = "The field must be a 5-digit number between 10000 and 99999.")]
        public int InviteCode { get; set; }
    }
}
