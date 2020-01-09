using System.ComponentModel.DataAnnotations;

namespace Forum.API.ViewModels
{
    public class CreateGategoryViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public long SectionId { get; set; }
    }
}
