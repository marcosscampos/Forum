using System.ComponentModel.DataAnnotations;

namespace Forum.Client.Models
{
    public class CreateReplyViewModel
    {
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public long TopicId { get; set; }

        public long UserId { get; set; }
    }
}
