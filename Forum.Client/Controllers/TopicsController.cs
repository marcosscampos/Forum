using System.Threading.Tasks;
using Forum.Client.Models;
using Forum.Infra.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Forum.Client.Controllers
{
    [Route("topics")]
    public class TopicsController : Controller
    {
        private readonly ApiService _api;

        public TopicsController(ApiService api)
        {
            _api = api;
        }

        [HttpGet("novo-topico")]
        public async Task<IActionResult> Create()
        {
            var categories = await _api.GetCategories();

            var vm = new CreateTopicViewModel
            {
                CategoriesAvailable = new SelectList(categories, "Id", "Name", "Section.Name")
            };

            return View(vm);
        }

        [HttpPost("novo-topico")]
        public async Task<IActionResult> Create(CreateTopicViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var currentUser = await _api.GetCurrentLoggedUser(accessToken);

            vm.UserId = currentUser.Id;

            await _api.PostTopic(vm, accessToken);

            return RedirectToAction("Index", "Categories");
        }

        [HttpGet("id:long")]
        public async Task<IActionResult> Details(long id)
        {
            var topics = await _api.GetTopicById(id);

            return View(topics);
        }
    }
}