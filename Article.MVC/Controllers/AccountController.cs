using Article.MVC.Context;
using Article.MVC.Entities;
using Article.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Article.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IdentityContext _dbContext;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IdentityContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public IActionResult Create()
        {
            return View(new UserCreateModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
               string? companyName = _dbContext.Invites
                                        .Join(_dbContext.Companies, i => i.CompanyId, c => c.Id, (i, c) => new { Invite = i, Company = c })
                                        .Where(x => x.Invite.InviteCode == model.InviteCode)
                                        .Select(x => x.Company.Name)
                                        .SingleOrDefault();

                if (companyName == null)
                {
                    ModelState.AddModelError(string.Empty, "Invite Code is not matched any companies.");
                    return View(model);
                }
                
                AppUser user = new()
                {
                    UserName = model.Username,
                    Email = model.Email,
                    CompanyName = companyName
                };
                var identityResult = await _userManager.CreateAsync(user, model.Password);
                if (identityResult.Succeeded)
                {
                    var memberRole = await _roleManager.FindByNameAsync("Member");
                    if (memberRole == null)
                    {
                        await _roleManager.CreateAsync(new()
                        {
                            Name = "Member",
                            CreatedTime= DateTime.Now
                        });
                    }

                    await _userManager.AddToRoleAsync(user, "Member");

                    return RedirectToAction("Index", "Home");
                }
                foreach(var error in identityResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        public IActionResult SignIn(string returnUrl)
        {
            return View(new UserSignInModel{ ReturnUrl = returnUrl});
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInModel model)
        {
            if(ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, true);
                if (signInResult.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    var user = await _userManager.FindByNameAsync(model.Username);
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("AdminPanel");
                    }
                    else
                    {
                        return RedirectToAction("Panel");
                    }
                    
                }
                //else if(signInResult.IsLockedOut) 
                //{
                //    //account locked
                //}
                //else if (signInResult.IsNotAllowed)
                //{
                //    //email or phonenumber check. Send activation link to mail.
                //}
            }
            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> GetUserList()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }
        [Authorize(Roles = "Member")]
        public IActionResult Panel()
        {
            return View();
        }

        public async Task<IActionResult> AssignRole(int id)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == id);
            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = _roleManager.Roles.ToList();

            RoleAssignSendModel model = new RoleAssignSendModel();

            List<RoleAssignListModel> list = new List<RoleAssignListModel>();
            foreach (var role in roles)
            {
                list.Add(new()
                {
                    Name = role.Name,
                    RoleId = role.Id,
                    Exist = userRoles.Contains(role.Name)
                });
            }

            model.Roles = list;
            model.UserId = id;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Assignrole(RoleAssignSendModel model)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == model.UserId);
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in model.Roles)
            {
                if (role.Exist) // Rol tickli olarak gelmiş demektir.
                {
                    if (!userRoles.Contains(role.Name)) //Eğer o rol o userda yoksa 
                        await _userManager.AddToRoleAsync(user, role.Name);
                }
                else // Rol ticklemesi kaldırılmış ya da zaten yoktu
                {
                    if (userRoles.Contains(role.Name)) //Eğer o rol userda varsa
                        await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }
            return RedirectToAction("GetUserList");
        }

    }
}
