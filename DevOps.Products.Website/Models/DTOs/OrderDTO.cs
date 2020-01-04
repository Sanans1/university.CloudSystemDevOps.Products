using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevOps.Products.Website.Models.DTOs
{
    public class OrderDTO
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string CustomerUsername { get; set; }
    }
}
