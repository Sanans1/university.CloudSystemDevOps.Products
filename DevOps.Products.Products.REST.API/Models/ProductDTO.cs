﻿using System.Collections.Generic;

namespace DevOps.Products.Products.REST.API.Models
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public int BrandID { get; set; }
        public string BrandName { get; set; }

        public List<PriceHistoryDTO> PriceHistories { get; set; }
    }
}
