using System.ComponentModel.DataAnnotations;

namespace Forum.Client.Models
{
    public class CreateSectionViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
