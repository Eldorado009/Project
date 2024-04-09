using AllupProjectMVC.Models;
using AllupProjectMVC.ViewModels.Wishlist;

namespace AllupProjectMVC.Business.Interfaces
{
    public interface IWishlistService
    {
        int AddToWishlist(int id, Product product);
        int GetCount();
        Task<List<WishlistDetailVM>> GetWishlistDatasAsync();
        void DeleteItem(int id);
        List<WishlistVM> GetDatasFromCookies();
        Task<Wishlist> GetByUserIdAsync(string userId);
        Task<int> GetById(int id);
        Task<List<WishlistProduct>> GetAllByWishlistIdAsync(int? basketId);
    }
}
