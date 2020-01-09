using System.ComponentModel.DataAnnotations;

namespace Forum.API.ViewModels
{
    public class EditCategoryViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public long SectionId { get; set; }
    }
}
