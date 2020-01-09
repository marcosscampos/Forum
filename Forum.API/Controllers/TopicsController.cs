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
    [Route("api/topics")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicsRepository _topicsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IRepliesRepository _repliesRepository;
        private readonly IMapper _mapper;

        public TopicsController(ITopicsRepository topicsRepository,
            IMapper mapper,
            IUsersRepository usersRepository,
            ICategoriesRepository categoriesRepository,
            IRepliesRepository repliesRepository)
        {
            _topicsRepository = topicsRepository;
            _usersRepository = usersRepository;
            _categoriesRepository = categoriesRepository;
            _repliesRepository = repliesRepository;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var topics = await _topicsRepository.GetAllAsync();

            return Ok(topics);
        }

        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> Index([FromBody] CreateTopicViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(v => v.Errors));

            var categoryExist = await _categoriesRepository.Contains(vm.CategoryId);

            if (categoryExist)
            {
                var topic = _mapper.Map<Topic>(vm);

                await _topicsRepository.AddAsync(topic);

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var topic = await _topicsRepository.GetByIdAsync(id);

            if (topic != null)
                return Ok(topic);

            return NotFound();
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Edit(long id, [FromBody] EditTopicViewModel vm)
        {
            var topic = await _topicsRepository.GetByIdAsync(id);

            if (topic != null)
            {
                topic.Title = vm.Title;
                topic.Content = vm.Content;
                topic.CategoryId = vm.CategoryId;

                await _topicsRepository.UpdateAsync(topic);

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Remove(long id)
        {
            var topicToBeDeleted = await _topicsRepository.GetByIdAsync(id);

            if (topicToBeDeleted != null)
            {
                await _topicsRepository.RemoveAsync(topicToBeDeleted);

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("{id:long}/replies)")]
        public async Task<IActionResult> GetReplies(long id)
        {
            var topic = await _topicsRepository.GetByIdAsync(id);

            if (topic != null)
            {
                var replies = topic.Replies;

                return Ok(replies);
            }

            return BadRequest();
        }

        [HttpPost("{id:long}/replies")]
        [Authorize]
        public async Task<IActionResult> CreateReply(long id, [FromBody] CreateReplyViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(v => v.Errors));

            var topicExist = await _topicsRepository.Contains(vm.TopicId);
            var userExist = await _topicsRepository.Contains(vm.UserId);

            if (topicExist)
            {
                var reply = _mapper.Map<Reply>(vm);

                await _repliesRepository.AddAsync(reply);

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("{topicId:long}/replies/{replyId:long}")]
        public async Task<IActionResult> GetById(long topicId, long replyId)
        {
            var topic = await _topicsRepository.GetByIdAsync(topicId);

            if (topic != null)
            {
                var reply = _repliesRepository.GetByIdAsync(replyId);

                if (reply != null)
                {
                    return Ok(reply);
                }

                return NotFound();
            }
                

            return NotFound();
        }

        [HttpPut("{topicId:long}/replies/{replyId:long}")]
        [Authorize]
        public async Task<IActionResult> Edit(long topicId, long replyId, [FromBody] EditReplyViewModel vm)
        {
            var topic = await _topicsRepository.GetByIdAsync(topicId);

            if (topic != null)
            {
                var reply = await _repliesRepository.GetByIdAsync(replyId);

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

            return BadRequest();
        }

        [HttpPut("{topicId:long}/replies/{replyId:long}")]
        [Authorize]
        public async Task<IActionResult> Remove(long topicId, long replyId)
        {
            var topic = _topicsRepository.GetByIdAsync(topicId);

            if (topic != null)
            {
                var replyToBeDeleted = await _repliesRepository.GetByIdAsync(replyId);

                if (replyToBeDeleted != null)
                {
                    await _repliesRepository.RemoveAsync(replyToBeDeleted);

                    return Ok();
                }

                return BadRequest();
            }

            return BadRequest();
        }
    }
}