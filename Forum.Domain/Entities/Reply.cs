using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Domain.Entities
{
    public class Reply : Entity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public long TopicId { get; set; }

        [Required]
        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }

        public long? UserId { get; set; }
     
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
