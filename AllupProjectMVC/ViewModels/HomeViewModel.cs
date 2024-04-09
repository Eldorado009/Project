using AllupProjectMVC.Areas.admin.ViewModels.Blog;
using AllupProjectMVC.Areas.admin.ViewModels.Category;
using AllupProjectMVC.Areas.admin.ViewModels.Product;
using AllupProjectMVC.Areas.admin.ViewModels.Slider;
using AllupProjectMVC.Models;
using System;

namespace AllupProjectMVC.ViewModels;

public class HomeViewModel
{
    public List<Feature> Features { get; set; }
    public List<Banner> Banners { get; set; }
    public List<SliderVM> Sliders { get; set; }
    public List<CategoryVM> Categories { get; set; }
    public List<ProductVM> Products { get; set; }
    public List<Blog> Blogs { get; set; }
    public bool IsInWishlist { get; set; }
}
