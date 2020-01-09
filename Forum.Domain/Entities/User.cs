using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.Domain.Entities
{
    public class User : Entity
    {
        public User()
        {
            Topics = new HashSet<Topic>();
            Replies = new HashSet<Reply>();
        }

        public string AvatarUri { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public ICollection<Topic> Topics { get; set; }

        public ICollection<Reply> Replies { get; set; }
    }
}
