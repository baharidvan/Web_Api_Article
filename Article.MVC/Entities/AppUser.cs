using Microsoft.AspNetCore.Identity;

namespace Article.MVC.Entities
{
    public class AppUser:IdentityUser<int>
    {
        public string CompanyName { get; set; }
        public int? InviteCode { get; set; }
    }
}
