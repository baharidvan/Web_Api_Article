using Books.WebApi.Data;
using Microsoft.Identity.Client;

namespace Books.WebApi.Interfaces
{
    public interface IRepository
    {
        public Task<List<Article>> GetAllAsync();
        public Task<Article> GetByIdAsync(int id);
        public Task<Article> CreateAsync(Article article);
        public Task UpdateAsync(Article article);
        public Task DeleteAsync(int id);
    }
}
