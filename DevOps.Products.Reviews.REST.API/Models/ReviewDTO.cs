using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevOps.Products.Reviews.REST.API.Models
{
    public class ReviewDTO
    {
        public int ID { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }

        public int ProductID { get; set; }
        public string CustomerUsername { get; set; }
    }
}
