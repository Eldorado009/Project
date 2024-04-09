using AllupProjectMVC.Areas.admin.ViewModels.Blog;
using AllupProjectMVC.Models;
using System.Linq.Expressions;

namespace AllupProjectMVC.Business.Interfaces
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllAsync(Expression<Func<Blog, bool>>? expression = null, params string[] includes);
        Task<BlogVM> GetByIdAsync(int id);
        Task CreateAsync(BlogCreateVM request);
        Task DeleteAsync(int id);
        Task UpdateAsync(BlogUpdateVM request);
    }
}
