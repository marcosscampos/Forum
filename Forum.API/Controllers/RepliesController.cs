using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Forum.API.ViewModels;
using Forum.Domain.Entities;
using Forum.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [Route("api/replies")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepliesRepository _repliesRepository;
        private readonly ITopicsRepository _topicsRepository;
        private readonly IUsersRepository _usersRepository;

        public RepliesController(IRepliesRepository repliesRepository, ITopicsRepository topicsRepository, IUsersRepository usersRepository, IMapper mapper)
        {
            _repliesRepository = repliesRepository;
            _topicsRepository = topicsRepository;
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var replies = await _repliesRepository.GetAllAsync();

            return Ok(replies);
        }

        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> Index([FromBody] CreateReplyViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(v => v.Errors));

            var sectionExist = await _topicsRepository.Contains(vm.TopicId);
            var userExist = await _usersRepository.Contains(vm.UserId);

            if (sectionExist)
            {
                var reply = _mapper.Map<Reply>(vm);

                await _repliesRepository.AddAsync(reply);

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var reply = await _repliesRepository.GetByIdAsync(id);

            if (reply != null)
                return Ok(reply);

            return NotFound();
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Edit(long id, [FromBody] EditReplyViewModel vm)
        {
            var reply = await _repliesRepository.GetByIdAsync(id);

            if (reply != null)
            {
                reply.Title = vm.Title;
                reply.Content = vm.Content;
                reply.TopicId = vm.TopicId;
                reply.UserId = vm.UserId;

                await _repliesRepository.UpdateAsync(reply);

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Remove(long id)
        {
            var replyToBeDeleted = await _repliesRepository.GetByIdAsync(id);

            if (replyToBeDeleted != null)
            {
                await _repliesRepository.RemoveAsync(replyToBeDeleted);

                return Ok();
            }

            return BadRequest();
        }
    }
}