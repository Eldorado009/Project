using System.ComponentModel.DataAnnotations;

namespace AllupProjectMVC.Models;

public class Category : BaseEntity
{
    
    [StringLength(100)]
    public string Image { get; set; }
    [Required]
    [StringLength(20)]
    public string Name { get; set; }
    public List<Product> Products { get; set; }
}
