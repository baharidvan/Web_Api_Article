using Books.WebApi.Data;
using Books.WebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Books.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class ArticlesController : ControllerBase
    {
        private readonly IRepository _repository;

        public ArticlesController(IRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _repository.GetAllAsync();
            if (articles == null)
            {
                ModelState.AddModelError("CustomError", "Articles not found.");
                return NotFound(ModelState);
            }
            return Ok(articles);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetArticle(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var article = await _repository.GetByIdAsync(id);
            if (article == null)
            {
                ModelState.AddModelError("CustomError", "Article not found.");
                return NotFound(ModelState);
            }
            return Ok(article);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateArticle(Article article)
        {
            var addedArticle = await _repository.CreateAsync(article);
            return Created(String.Empty, addedArticle);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArticle(Article article)
        {
            var checkArticle = _repository.GetByIdAsync(article.Id);
            if (checkArticle == null)
            {
                return NotFound(article.Id);
            }
            await _repository.UpdateAsync(article);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _repository.GetByIdAsync(id);
            if (article == null)
            {
                return NotFound(id);
            }
            await _repository.DeleteAsync(id);
            return NoContent();
        }

    }
}
