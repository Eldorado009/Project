using System.ComponentModel.DataAnnotations;

namespace AllupProjectMVC.Areas.admin.ViewModels.Slider
{
    public class SliderUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Title2 { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}
