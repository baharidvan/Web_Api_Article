using Article.MVC.Context;
using Article.MVC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 3;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<IdentityContext>();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.HttpOnly = true; //Javascript aracılığıyla çekilemiyor.
    opt.Cookie.SameSite = SameSiteMode.Strict; //Sadece ilgili domainde kullanılır.
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; //http ise http, https ise https
    opt.Cookie.Name = "IdentityCookie";
    opt.ExpireTimeSpan = TimeSpan.FromDays(10); //Kullanıcı bilgisini 10 gün boyunca hatırla
    opt.LoginPath = new PathString("/Account/SignIn");

});

builder.Services.AddDbContext<IdentityContext>(opt=>
{
    opt.UseSqlServer("server=(localdb)\\mssqllocaldb; database=IdentityDb; integrated security=true;");
});
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles(new StaticFileOptions
{
    RequestPath = "/node_modules",
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules"))
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapDefaultControllerRoute();

app.Run();
