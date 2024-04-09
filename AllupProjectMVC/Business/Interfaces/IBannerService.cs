using AllupProjectMVC.Areas.admin.ViewModels.Banner;
using AllupProjectMVC.Models;
using System.Linq.Expressions;

namespace AllupProjectMVC.Business.Interfaces
{
    public interface IBannerService
    {
        Task<List<Banner>> GetAllAsync(Expression<Func<Banner, bool>>? expression = null, params string[] includes);      
        Task<BannerVM> GetByIdAsync(int id);
        Task UpdateAsync(BannerUpdateVM request);   
    }
}
