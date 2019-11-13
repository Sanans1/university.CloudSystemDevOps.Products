using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public CustomerViewModel Customer { get; set; }
    }
}
