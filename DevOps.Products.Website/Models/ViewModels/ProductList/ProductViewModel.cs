﻿namespace DevOps.Products.Website.Models.ViewModels.ProductList
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public int BrandID { get; set; }
        public string BrandName { get; set; }
    }
}
