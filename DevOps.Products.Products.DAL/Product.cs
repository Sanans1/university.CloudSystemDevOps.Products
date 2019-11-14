using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Products.Products.DAL
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int CategoryID { get; set; }
        public int BrandID { get; set; }

        public bool IsActive { get; set; }
    }
}
