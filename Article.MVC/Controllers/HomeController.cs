
using Article.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;

namespace Article.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        private readonly IHttpClientFactory httpClientFactory;

        public HomeController(IHttpClientFactory _httpClientFactory)
        {
            httpClientFactory = _httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5281/api/articles");

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ArticleResponseModel>>(jsonData);
                return View(result);
            }
            else
            {
                return View(null);
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult CreateArticle()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateArticle(ArticleRequestModel model)
        {
            var client = httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(jsonData, Encoding.UTF8,"application/json");
            var responseMessage = await client.PostAsync("http://localhost:5281/api/articles", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["errorMessage"] = $"An error was encountered. Error Code {(int)responseMessage.StatusCode}";
                return View(responseMessage);
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateArticle(int id)
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5281/api/articles/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ArticleRequestModel>(jsonData);
                return View(result);
            }
            else
            {
                return View(null);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateArticle(ArticleRequestModel model)
        {
            var client = httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:5281/api/articles", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveArticle(int id)
        {
            var client = httpClientFactory.CreateClient();
            await client.DeleteAsync($"http://localhost:5281/api/articles/{id}");
            return RedirectToAction("Index");
        }


    }
}