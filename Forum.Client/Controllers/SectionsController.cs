using System.Threading.Tasks;
using Forum.Client.Models;
using Forum.Infra.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Client.Controllers
{
    [Route("sections")]
    public class SectionsController : Controller
    {
        private readonly ApiService _api;

        public SectionsController(ApiService api)
        {
            _api = api;
        }

        [HttpGet("new")]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("new")]
        [Authorize]
        public async Task<IActionResult> Create(CreateSectionViewModel vm, string redirectuUri)
        {
            if (!ModelState.IsValid) return View(vm);

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            await _api.PostSection(vm, accessToken);

            return RedirectToAction("Index", "Categories");
        }
    }
}