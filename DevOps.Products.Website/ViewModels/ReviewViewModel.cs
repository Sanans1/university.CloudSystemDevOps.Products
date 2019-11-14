using System.ComponentModel.DataAnnotations;

namespace DevOps.Products.Website.ViewModels
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

        public int ProductID { get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}
