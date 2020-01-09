using System.ComponentModel.DataAnnotations;

namespace Forum.API.ViewModels
{
    public class EditTopicViewModel
    {
        [Required]
        [MinLength(10)]
        [MaxLength(125)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        public string Content { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public long CategoryId { get; set; }
    }
}
