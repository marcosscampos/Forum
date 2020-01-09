using System.ComponentModel.DataAnnotations;

namespace Forum.API.ViewModels
{
    public class EditReplyViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public long TopicId { get; set; }

        [Required]
        public long UserId { get; set; }
    }
}
