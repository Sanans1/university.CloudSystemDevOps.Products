using System.ComponentModel.DataAnnotations;

namespace DevOps.Products.Website.Models.ViewModels
{
    public class BrandViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
