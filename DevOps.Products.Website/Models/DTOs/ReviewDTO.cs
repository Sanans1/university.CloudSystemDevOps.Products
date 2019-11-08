using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevOps.Products.Website.Models.DTOs
{
    public class ReviewDTO
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public int ProductID { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
    }
}
