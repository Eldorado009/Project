using AllupProjectMVC.Areas.admin.ViewModels.Product;
using AllupProjectMVC.ViewModels.Basket;

namespace AllupProjectMVC.ViewModels
{
    public class BasketPageVM
    {
        public List<BasketDetailVm> BasketDetails { get; set; }
        public List<ProductVM> Products { get; set; }
    }
}
