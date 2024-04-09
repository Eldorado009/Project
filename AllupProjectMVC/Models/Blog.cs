using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllupProjectMVC.Models
{
    public class Blog : BaseEntity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
