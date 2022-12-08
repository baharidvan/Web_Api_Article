using Books.WebApi.Data;
using Books.WebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books.WebApi.Repositories
{
    public class ArticleRepository : IRepository
    {
        private readonly ArticleContext _context;
        public ArticleRepository(ArticleContext context)
        {
            _context = context;
        }

        public async Task<List<Article>> GetAllAsync()
        {
            return await _context.Articles.AsNoTracking().ToListAsync();
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            return await _context.Articles.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Article> CreateAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();

            return article;
        }

        public async Task UpdateAsync(Article article)
        {
            var unchangedArticle = await _context.Articles.FindAsync(article.Id);
            _context.Articles.Entry(unchangedArticle).CurrentValues.SetValues(article);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }
    }
}
