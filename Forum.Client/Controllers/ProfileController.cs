using System.Threading.Tasks;
using Forum.Client.Models;
using Forum.Infra.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Client.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApiService _api;
        public ProfileController(ApiService api)
        {
            _api = api;
        }

        [Authorize]
        [HttpGet("perfil")]
        public async Task<IActionResult> MyProfile()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var profileUser = await _api.GetCurrentLoggedUser(accessToken);

            return View(profileUser);
        }

        [HttpGet("perfil/edit")]
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var profile = new EditUserViewModel();

            return View(profile);
        }

        [HttpPost("perfil/edit")]
        [Authorize]
        public async Task<IActionResult> Edit(EditUserViewModel vm)
        {
            return View();
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Categories");
        }

    }
}