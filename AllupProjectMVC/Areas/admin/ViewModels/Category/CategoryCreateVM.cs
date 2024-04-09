using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AllupProjectMVC.Areas.admin.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
