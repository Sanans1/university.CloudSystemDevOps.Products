using System;
using System.ComponentModel.DataAnnotations.Schema;
using DevOps.Products.Common.Repository;

namespace DevOps.Products.Products.DAL
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int CategoryID { get; set; }
        public int BrandID { get; set; }
    }
}
