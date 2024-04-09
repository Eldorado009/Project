using AllupProjectMVC.Business.Interfaces;
using AllupProjectMVC.Data;
using AllupProjectMVC.Helpers.Responses;
using AllupProjectMVC.Models;
using AllupProjectMVC.ViewModels.Basket;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace AllupProjectMVC.Business.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AllupDbContext _context;
        private readonly IProductService _productService;

        public BasketService(IHttpContextAccessor httpContextAccessor,
                           AllupDbContext context,
                           IProductService productService)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _productService = productService;

        }
        public List<BasketVM> GetDatasFromCookie()
        {
            List<BasketVM> baskets;
            if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                baskets = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                baskets = new List<BasketVM>();
            }
            return baskets;
        }

        public void SetDatasToCookie(List<BasketVM> baskets, Product dbProduct, BasketVM existProduct)
        {
            if (existProduct == null)
            {
                baskets.Add(new BasketVM
                {
                    ProductId = dbProduct.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }
            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(baskets));
        }

        public void AddToBasket(int id, Product product)
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

            BasketVM existProducts = basket.FirstOrDefault(m => m.ProductId == product.Id);

            if (existProducts is null)
            {
                basket.Add(new BasketVM { ProductId = product.Id, Count = 1 });
            }
            else
            {
                existProducts.Count++;

            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
        }

        public int GetCount()
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
            return basket.Sum(m => m.Count);

        }

        public async Task<List<BasketDetailVm>> GetBasketDatasAsync()
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
                    Image = existProduct.Images.FirstOrDefault().Image
                });
            }
            return basketDetailList;
        }


        public async Task<DeleteBasketItemResponse> DeleteItem(int id)
        {
            List<decimal> grandTotal = new();

            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);

            BasketVM basketItem = basket.FirstOrDefault(m => m.ProductId == id);

            basket.Remove(basketItem);

            foreach (var item in basket)
            {
                var product = await _productService.GetByIdWithIncludesAsync(item.ProductId);


                decimal total = item.Count * product.Price;

                grandTotal.Add(total);
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            return new DeleteBasketItemResponse
            {
                Count = basket.Sum(m => m.Count),
                GrandTotal = grandTotal.Sum()
            };
        }


        public async Task<CountResponse> IncreaseProductCount(int id)
        {
            List<decimal> grandTotal = new();

            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);

            BasketVM existProduct = basket.FirstOrDefault(m => m.ProductId == id);

            existProduct.Count++;

            var basketItem = await _productService.GetByIdWithIncludesAsync(id);

            var productTotalPrice = existProduct.Count * basketItem.Price;

            foreach (var item in basket)
            {

                var product = await _productService.GetByIdWithIncludesAsync(item.ProductId);

                decimal total = item.Count * product.Price;

                grandTotal.Add(total);
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            return new CountResponse
            {
                CountItem = existProduct.Count,
                GrandTotal = grandTotal.Sum(),
                ProductTotalPrice = productTotalPrice,
                CountBasket = basket.Sum(m => m.Count)

            };
        }


        public async Task<CountResponse> DecreaseProductCount(int id)
        {
            List<decimal> grandTotal = new();

            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            BasketVM existProduct = basket.FirstOrDefault(m => m.ProductId == id);


            if (existProduct.Count > 1)
            {

                existProduct.Count--;


            }
            foreach (var item in basket)
            {

                var product = await _productService.GetByIdWithIncludesAsync(item.ProductId);

                decimal total = item.Count * product.Price;

                grandTotal.Add(total);
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            var basketItem = await _productService.GetByIdWithIncludesAsync(id);
            var productTotalPrice = existProduct.Count * basketItem.Price;
            return new CountResponse
            {
                CountItem = existProduct.Count,
                GrandTotal = grandTotal.Sum(),
                ProductTotalPrice = productTotalPrice,
                CountBasket = basket.Sum(m => m.Count)
            };
        }


        public async Task<Basket> GetByUserIdAsync(string userId)
        {
            var data = await _context.Baskets.Include(m => m.BasketProducts).FirstOrDefaultAsync(m => m.AppUserId == userId);
            return data;
        }

        public async Task<List<BasketProduct>> GetAllByBasketIdAsync(int? basketId)
        {
            return await _context.BasketProducts.Where(m => m.BasketId == basketId).ToListAsync();
        }


        public List<BasketVM> GetDatasFromCookies()
        {
            var data = _httpContextAccessor.HttpContext.Request.Cookies["basket"];

            if (data is not null)
            {
                var basket = JsonConvert.DeserializeObject<List<BasketVM>>(data);
                return basket;
            }
            else
            {
                return new List<BasketVM>();
            }

        }
    }
}
