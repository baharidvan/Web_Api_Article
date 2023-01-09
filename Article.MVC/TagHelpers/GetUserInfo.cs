using Article.MVC.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace Article.MVC.TagHelpers
{
    [HtmlTargetElement("getUserInfo")]
    public class GetUserInfo:TagHelper
    {
        private readonly UserManager<AppUser> _userManager;
        public int UserId { get; set; }
        public GetUserInfo(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var html = "";
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == UserId);
            var roles = await _userManager.GetRolesAsync(user);

            var lastRole = roles.Last();
            foreach (var role in roles)
            {
                if (!role.Equals(lastRole))
                    html += role + ", ";
                else
                    html += role;
            }

            output.Content.SetHtmlContent(html);
        }
    }
}   
