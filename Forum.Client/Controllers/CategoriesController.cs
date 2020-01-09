using System.Linq;
using System.Threading.Tasks;
using Forum.Client.Models;
using Forum.Infra.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Forum.Client.Controllers
{
    [Route("")]
    public class CategoriesController : Controller
    {
        private readonly ApiService _api;

        public CategoriesController(ApiService api)
        {
            _api = api;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var categories = await _api.GetCategories();
            var categoriesGroupedBySection = categories.GroupBy(c => c.Section);

            return View(categoriesGroupedBySection);
        }

        [HttpGet("nova-categoria")]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var sectionsAvailable = await _api.GetSections();

            var vm = new CreateCategoryViewModel
            {
                SectionsAvailable = new SelectList(sectionsAvailable, "Id", "Name")
            };

            return View(vm);
        }

        [HttpPost("nova-categoria")]
        [Authorize]
        public async Task<IActionResult> Create(CreateCategoryViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            await _api.PostCategory(vm, accessToken);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> Details(long id)
        {
            var topics = await _api.GetTopicsFromCategory(id);

            return View(topics);
        }
    }
}