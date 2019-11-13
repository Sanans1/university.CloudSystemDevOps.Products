using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevOps.Products.Website.Models.ViewModels
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }

        public CategoryViewModel Category { get; set; }
        public BrandViewModel Brand { get; set; }
    }
}
