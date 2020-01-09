using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Forum.Client.Models
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public long SectionId { get; set; }

        public SelectList SectionsAvailable { get; set; }
    }
}
