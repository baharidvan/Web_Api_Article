using Article.MVC.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Article.MVC.Context
{
    public class IdentityContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(new Company[]
            {
                new(){Id=1, Name= "Company_A"},
                new(){Id=2, Name= "Company_B"},
                new(){Id=3, Name= "Company_C"}
            });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Invite> Invites { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
