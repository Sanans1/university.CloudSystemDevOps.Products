using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;

namespace DevOps.Products.Website.Models.ViewModels
{
    public class ReviewViewModel
    {
        public ReviewViewModel() { }

        public ReviewViewModel(ReviewDTO reviewDto)
        {
            ID = reviewDto.ID;
            CustomerName = reviewDto.CustomerName;
            Rating = reviewDto.Rating;
            Text = reviewDto.Text;
        }

        public int ID { get; set; }
        public string CustomerName { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }

        public int ProductID { get; set; }
    }
}
