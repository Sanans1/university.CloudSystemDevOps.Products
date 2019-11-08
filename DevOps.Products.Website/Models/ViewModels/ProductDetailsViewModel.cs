using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;

namespace DevOps.Products.Website.Models.ViewModels
{
    public class ProductDetailsViewModel
    {
        public ProductDetailsViewModel() { }

        public ProductDetailsViewModel(ProductDTO productDto)
        {
            ID = productDto.ID;
            Name = productDto.Name;
            Brand = productDto.Brand;
            Category = productDto.Category;
            Description = productDto.Description;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public ReviewViewModel[] ReviewViewModels { get; set; }
    }
}
