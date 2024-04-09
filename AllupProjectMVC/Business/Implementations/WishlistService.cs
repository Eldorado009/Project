using AllupProjectMVC.Business.Interfaces;
using AllupProjectMVC.Data;
using AllupProjectMVC.Models;
using AllupProjectMVC.ViewModels.Wishlist;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace AllupProjectMVC.Business.Implementations
{
    public class WishlistService : IWishlistService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductService _productService;
        private readonly AllupDbContext _context;
        public WishlistService(IHttpContextAccessor httpContextAccessor, IProductService productService,
                                                                          AllupDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
            _context = context;
        }

        public int AddToWishlist(int id, Product product)
        {
            List<WishlistVM> wishlist;

            if (_httpContextAccessor.HttpContext.Request.Cookies["wishlist"] != null)
            {
                wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(_httpContextAccessor.HttpContext.Request.Cookies["wishlist"]);
            }
            else
            {
                wishlist = new List<WishlistVM>();
            }



            WishlistVM existProducts = wishlist.FirstOrDefault(m => m.ProductId == product.Id);

            if (existProducts is null)
            {
                wishlist.Add(new WishlistVM { ProductId = product.Id });
            }


            _httpContextAccessor.HttpContext.Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlist));

            return wishlist.Count();
        }

        public async Task<List<WishlistDetailVM>> GetWishlistDatasAsync()
        {
            List<WishlistVM> wishlist;

            if (_httpContextAccessor.HttpContext.Request.Cookies["wishlist"] != null)
            {
                wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(_httpContextAccessor.HttpContext.Request.Cookies["wishlist"]);
            }
            else
            {
                wishlist = new List<WishlistVM>();

            }

            List<WishlistDetailVM> wishlistDetails = new();
            foreach (var item in wishlist)
            {
                Product existProduct = await _productService.GetByIdWithIncludesAsync(item.ProductId);

                wishlistDetails.Add(new WishlistDetailVM
                {
                    Id = existProduct.Id,
                    Name = existProduct.Name,
                    Price = existProduct.Price,
                    Image = existProduct.Images.FirstOrDefault().Image
                });
            }
            return wishlistDetails;
        }

        public int GetCount()
        {
            List<WishlistVM> wishlist;

            if (_httpContextAccessor.HttpContext.Request.Cookies["wishlist"] != null)
            {
                wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(_httpContextAccessor.HttpContext.Request.Cookies["wishlist"]);
            }
            else
            {
                wishlist = new List<WishlistVM>();
            }
            return wishlist.Count();

        }

        public void DeleteItem(int id)
        {

            List<WishlistVM> wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(_httpContextAccessor.HttpContext.Request.Cookies["wishlist"]);

            WishlistVM wishlistItem = wishlist.FirstOrDefault(m => m.ProductId == id);

            wishlist.Remove(wishlistItem);

            _httpContextAccessor.HttpContext.Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlist));

        }

        public List<WishlistVM> GetDatasFromCookies()
        {
            var data = _httpContextAccessor.HttpContext.Request.Cookies["wishlist"];

            if (data is not null)
            {
                var wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(data);
                return wishlist;
            }
            else
            {
                return new List<WishlistVM>();
            }

        }

        public async Task<Wishlist> GetByUserIdAsync(string userId)
        {
            var data = await _context.Wishlists.Include(m => m.WishlistProducts).FirstOrDefaultAsync(m => m.AppUserId == userId);
            return data;
        }
        public async Task<List<WishlistProduct>> GetAllByWishlistIdAsync(int? basketId)
        {
            return await _context.WishlistProducts.Where(m => m.WishlistId == basketId).ToListAsync();
        }

        public async Task<int> GetById(int id)
        {
            Wishlist wishlist = await _context.Wishlists.FirstOrDefaultAsync(m => m.Id == id);
            return wishlist.Id;
        }
    }
}
