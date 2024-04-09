using AllupProjectMVC.Helpers.Responses;
using AllupProjectMVC.Models;
using AllupProjectMVC.ViewModels.Basket;

namespace AllupProjectMVC.Business.Interfaces
{
    public interface IBasketService
    {
        void AddToBasket(int id, Product product);
        List<BasketVM> GetDatasFromCookies();
        void SetDatasToCookie(List<BasketVM> baskets, Product dbProduct, BasketVM existProduct);
        int GetCount();
        Task<List<BasketDetailVm>> GetBasketDatasAsync();
        Task<DeleteBasketItemResponse> DeleteItem(int id);
        Task<CountResponse> IncreaseProductCount(int id);
        Task<CountResponse> DecreaseProductCount(int id);
        Task<Basket> GetByUserIdAsync(string userId);
        Task<List<BasketProduct>> GetAllByBasketIdAsync(int? basketId);
    }
}
