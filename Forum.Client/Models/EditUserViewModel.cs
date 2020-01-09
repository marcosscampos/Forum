using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Forum.Client.Models
{
    public class EditUserViewModel
    {
        [Required]
        public IFormFile Avatar { get; set; }

        public string ProfileImageUrl { get; set; }
    }
}
