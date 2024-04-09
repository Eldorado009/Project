namespace AllupProjectMVC.Areas.admin.ViewModels.Banner
{
    public class BannerUpdateVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}
