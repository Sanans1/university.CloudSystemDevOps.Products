using System.ComponentModel.DataAnnotations;

namespace DevOps.Products.Website.Models.ViewModels
{
    public class CustomerViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
