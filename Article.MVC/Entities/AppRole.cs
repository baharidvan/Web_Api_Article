using Microsoft.AspNetCore.Identity;

namespace Article.MVC.Entities
{
    public class AppRole:IdentityRole<int>
    {
        public DateTime CreatedTime { get; set; }
    }
}
