using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Domain.Entities
{
    public class Topic : Entity
    {
        public Topic()
        {
            Replies = new HashSet<Reply>();
        }

        [MinLength(10)]
        [MaxLength(125)]
        public string Title { get; set; }

        [MinLength(10)]
        public string Content { get; set; }

        public long UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public long CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public ICollection<Reply> Replies { get; set; }
    }
}
