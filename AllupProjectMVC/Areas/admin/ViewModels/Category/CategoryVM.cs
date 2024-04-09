using AllupProjectMVC.Areas.admin.ViewModels.Product;
using System;

namespace AllupProjectMVC.Areas.admin.ViewModels.Category
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public List<ProductVM> Products { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
