using System.ComponentModel.DataAnnotations;

namespace AllupProjectMVC.Areas.admin.ViewModels.Blog
{
    public class BlogUpdateVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
