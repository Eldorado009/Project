using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllupProjectMVC.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<ProductImage> Images { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public List<WishlistProduct> WishlistProducts { get; set; }
        public List<BasketProduct> CartProducts { get; set; }
    }
}
