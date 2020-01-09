using System.Threading.Tasks;
using Forum.Client.Models;
using Forum.Infra.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Client.Controllers
{
    public class RepliesController : Controller
    {
        private readonly ApiService _api;

        public RepliesController(ApiService api)
        {
            _api = api;
        }

        [HttpGet("responder")]
        [Authorize]
        public async Task<IActionResult> Create(long topicId)
        {
            var topic = await _api.GetTopicById(topicId);

            var vm = new CreateReplyViewModel
            {
                Title = $"RES: {topic.Title}",
                TopicId = topic.Id
            };

            return View(vm);
        }

        [HttpPost("responder")]
        [Authorize]
        public async Task<IActionResult> Create(CreateReplyViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var currentUser = await _api.GetCurrentLoggedUser(accessToken);

            vm.UserId = currentUser.Id;

            await _api.PostReply(vm, accessToken);

            return RedirectToAction("Details", "Topics", new { id = vm.TopicId });
        }
    }
}