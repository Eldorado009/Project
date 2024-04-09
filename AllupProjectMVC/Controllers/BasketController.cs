using AllupProjectMVC.Areas.admin.ViewModels.Product;
using AllupProjectMVC.Business.Interfaces;
using AllupProjectMVC.Models;
using AllupProjectMVC.ViewModels;
using AllupProjectMVC.ViewModels.Basket;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AllupProjectMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartController(IBasketService basketService,
                              IProductService productService,
                              IHttpContextAccessor httpContextAccessor)
        {
            _basketService = basketService;
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<IActionResult> Index()
        {
            List<ProductVM> products = await _productService.GetAllAsync();
            var cartDatas = await _basketService.GetBasketDatasAsync();

            BasketPageVM model = new()
            {
                Products = products,
                BasketDetails = cartDatas
            };

            return View(model);
        }

        public async Task<IActionResult> GetSidebarProducts()
        {
            List<BasketVM> basket;

            if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();

            }

            List<BasketDetailVm> basketDetailList = new();
            foreach (var item in basket)
            {
                Product existProduct = await _productService.GetByIdWithIncludesAsync(item.ProductId);

                basketDetailList.Add(new BasketDetailVm
                {
                    Id = existProduct.Id,
                    Name = existProduct.Name,
                    Price = existProduct.Price,
                    Count = item.Count,
                    Total = existProduct.Price * item.Count,
                    Image = existProduct.Images.FirstOrDefault().Image,



                });
            }
            return Ok(basketDetailList);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var data = await _basketService.DeleteItem(id);

            return Ok(data);
        }


        [HttpPost]
        public async Task<IActionResult> IncreaseProductCount(int id)
        {
            var data = await _basketService.IncreaseProductCount(id);
            return Ok(data);
        }


        [HttpPost]
        public async Task<IActionResult> DecreaseProductCount(int id)
        {
            var data = await _basketService.DecreaseProductCount(id);
            return Ok(data);
        }
    }
}
