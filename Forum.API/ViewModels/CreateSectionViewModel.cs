using System.ComponentModel.DataAnnotations;

namespace Forum.API.ViewModels
{
    public class CreateSectionViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
