using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AllupProjectMVC.Areas.admin.ViewModels.Category
{
    public class CategoryUpdateVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [Required]
        public string Name { get; set; }
        public IFormFile Photo { get; set; }
        public IList<SelectListItem> Brands { get; set; }
    }
}
