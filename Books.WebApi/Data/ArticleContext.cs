using Microsoft.EntityFrameworkCore;

namespace Books.WebApi.Data
{
    public class ArticleContext:DbContext
    {
        public ArticleContext(DbContextOptions<ArticleContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().HasData(new Article[]
            {
                new(){Id=1, Name= "Savaşçı", Author="Doğan cüceloğlu", Publisher="Timaş", Price= 100},
                new(){Id=2, Name= "Osmanlı Tarihi", Author="Can Yılmaz", Publisher="Can", Price= 120},
                new(){Id=3, Name= "Savaş ve Barış", Author="Tolstoy", Publisher="Zeren", Price= 140},
                new(){Id=4, Name= "Kanlı Elmas", Author="Ahmet Akpınar", Publisher="Parıltı", Price= 200},
                new(){Id=5, Name= "Muhteşem İstanbul", Author="Volkan Kırat", Publisher="Levent", Price= 125}
            });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Article> Articles { get; set; }
    }
}
