using AllupProjectMVC.Business.Implementations;
using AllupProjectMVC.Business.Interfaces;
using AllupProjectMVC.Models;

namespace AllupProjectMVC
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services) 
        {
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IWishlistService, WishlistService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
