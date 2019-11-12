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

        public ProductDetailsViewModel(ProductDTO productDTO, IEnumerable<ReviewDTO> reviewDTOs, CustomerViewModel customerViewModel)
        {
            ID = productDTO.ID;
            Name = productDTO.Name;
            Brand = productDTO.Brand;
            Category = productDTO.Category;
            Description = productDTO.Description;

            ReviewViewModels = reviewDTOs.Select(review => new ReviewViewModel(review)).ToArray();
            CustomerViewModel = customerViewModel;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public ReviewViewModel[] ReviewViewModels { get; set; }
        public CustomerViewModel CustomerViewModel { get; set; }
    }
}
