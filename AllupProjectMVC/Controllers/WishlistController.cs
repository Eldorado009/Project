using AllupProjectMVC.Business.Interfaces;
using AllupProjectMVC.ViewModels.Wishlist;
using Microsoft.AspNetCore.Mvc;

namespace AllupProjectMVC.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;
        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _wishlistService.GetWishlistDatasAsync());
        }




        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            _wishlistService.DeleteItem(id);
            List<WishlistVM> wishlist = _wishlistService.GetDatasFromCookies();

            return Ok(wishlist.Count);
        }


    }
}
