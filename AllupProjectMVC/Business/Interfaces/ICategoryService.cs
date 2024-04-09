using AllupProjectMVC.Areas.admin.ViewModels.Category;
using AllupProjectMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace AllupProjectMVC.Business.Interfaces
{
    public interface ICategoryService 
    {
        Task<List<CategoryVM>> GetAllAsync();
        Task<CategoryVM> GetByIdAsync(int id);
        Task<CategoryVM> GetByNameWithoutTrackingAsync(string name);
        Task CreateAsync(CategoryCreateVM category);
        Task UpdateAsync(CategoryUpdateVM category);
        Task DeleteAsync(int id);
        List<SelectListItem> GetAllSelectedAsync();
    }
}
