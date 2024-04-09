using System.ComponentModel.DataAnnotations;

namespace AllupProjectMVC.Areas.admin.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Title2 { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
