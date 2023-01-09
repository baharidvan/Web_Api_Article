using Article.MVC.Context;
using Article.MVC.Entities;
using Article.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Article.MVC.Controllers
{
    public class InviteController : Controller
    {
        private readonly IdentityContext _dbContext;

        public InviteController(IdentityContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var list = _dbContext.Companies.ToList();
            var model = new List<CompanyListModel>();
            foreach (var company in list)
            {
                model.Add(new CompanyListModel
                {
                    Id = company.Id,
                    Name = company.Name
                });
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult GetInviteCode(CompanyListModel model)
        {
            Random random = new Random();
            bool check = true;
            int randomNumber = 0;
            while (check)
            {
                randomNumber = random.Next(10000, 100000);
                if (!_dbContext.Invites.Select(x => x.InviteCode).Contains(randomNumber))
                {
                    check= false;
                }
            }

            var invite = new Invite()
            {
                CompanyId = model.Id,
                InviteCode = randomNumber
            };

            _dbContext.Invites.Add(invite);
            _dbContext.SaveChanges();

            var inviteModel = new InviteCodeModel()
            {
                InviteCode= invite.InviteCode,
                CompanyName = model.Name
            };
            return View(inviteModel);
        }
    }
}
