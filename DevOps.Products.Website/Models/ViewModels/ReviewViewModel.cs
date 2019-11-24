using System.ComponentModel.DataAnnotations;

namespace DevOps.Products.Website.Models.ViewModels
{
    public class ReviewViewModel
    {
        public int ID { get; set; }
        [Required]
        [Range(1,5)]
        public int Rating { get; set; }
        [Required]
        [StringLength(280)]
        public string Text { get; set; }

        [Required]
        public int ProductID { get; set; }
        [Required]
        public CustomerViewModel Customer { get; set; }
    }
}
