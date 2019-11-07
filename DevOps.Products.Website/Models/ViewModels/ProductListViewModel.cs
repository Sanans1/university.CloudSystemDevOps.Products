using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;

namespace DevOps.Products.Website.Models.ViewModels
{
    public class ProductListViewModel
    {
        public ProductListViewModel() {}

        public ProductListViewModel(ProductDTO productDto)
        {
            ID = productDto.ID;
            Name = productDto.Name;
            Brand = productDto.Brand;
            Category = productDto.Category;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
    }
}
