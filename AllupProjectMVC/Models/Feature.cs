using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllupProjectMVC.Models;

public class Feature : BaseEntity
{
    [Required]
    [StringLength(15)]
    public string Title { get; set; }
    [NotMapped]
    public IFormFile? IconFile { get; set; }
    [Required]
    [StringLength(50)]
    public string Description { get; set; }
}
