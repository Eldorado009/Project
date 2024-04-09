using AllupProjectMVC.Areas.admin.ViewModels.Slider;

namespace AllupProjectMVC.Business.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderVM>> GetAllAsync();
        Task<SliderVM> GetByIdAsync(int id);
        Task CreateAsync(SliderCreateVM slider);
        Task UpdateAsync(SliderUpdateVM slider);
        Task DeleteAsync(int id);
    }
}
