using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        public int ProductID { get; set; }
        public string CustomerUsername { get; set; }
    }
}
