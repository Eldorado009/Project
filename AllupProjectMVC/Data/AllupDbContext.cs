using AllupProjectMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Newtonsoft.Json.Linq;

namespace AllupProjectMVC.Data;

public class AllupDbContext : IdentityDbContext<AppUser>
{
    public AllupDbContext(DbContextOptions<AllupDbContext> options) : base(options) { }

    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<WishlistProduct> WishlistProducts { get; set; }
    public DbSet<BasketProduct> BasketProducts { get; set; }
    public DbSet<Feature> Features { get; set; }
}
