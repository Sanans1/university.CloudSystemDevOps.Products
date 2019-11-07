using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Services.Interfaces;

namespace DevOps.Products.Website.Services.Mocks
{
    public class ProductFacadeServiceMock : IProductFacadeService
    {
        private IEnumerable<ProductDTO> _mockProductDtos;

        public ProductFacadeServiceMock()
        {
            _mockProductDtos = new List<ProductDTO>()
            {
                new ProductDTO()
                {
                    ID = 1,
                    Name = "Banana",
                    Brand = "Pablo's Farms",
                    Category = "Fruit",
                    Description = "Amazingly Tasty Bananas!"
                },
                new ProductDTO()
                {
                    ID = 2,
                    Name = "Orange",
                    Brand = "Pablo's Farms",
                    Category = "Fruit",
                    Description = "Amazingly Tasty Oranges!"
                },
                new ProductDTO()
                {
                    ID = 3,
                    Name = "Banana",
                    Brand = "Marge's Farms",
                    Category = "Fruit",
                    Description = "Amazingly Nasty Bananas!"
                },
                new ProductDTO()
                {
                    ID = 4,
                    Name = "Broccoli",
                    Brand = "Marge's Farms",
                    Category = "Vegetable",
                    Description = "Tastes like a mini tree!"
                },
            };
        }

        public async Task<IEnumerable<ProductDTO>> GetProductCollection(string name = null, string brand = null, string category = null)
        {
            return _mockProductDtos.Where(product => string.IsNullOrWhiteSpace(name) || product.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                                   .Where(product => string.IsNullOrWhiteSpace(brand) || product.Brand.Contains(brand, StringComparison.CurrentCultureIgnoreCase))
                                   .Where(product => string.IsNullOrWhiteSpace(category) || product.Category.Contains(category, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<ProductDTO> GetProduct(int id)
        {
            return _mockProductDtos.Single(product => product.ID == id);
        }
    }
}
