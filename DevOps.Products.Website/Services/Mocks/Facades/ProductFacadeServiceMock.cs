using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.DTOs;
using DevOps.Products.Website.Services.Interfaces.Facades;

namespace DevOps.Products.Website.Services.Mocks.Facades
{
    public class ProductFacadeServiceMock : IProductFacadeService
    {

        #region fields

        private readonly IEnumerable<ProductDTO> _mockProducts;
        private readonly IEnumerable<CategoryDTO> _mockCategories;
        private readonly IEnumerable<BrandDTO> _mockBrands;

        #endregion

        public ProductFacadeServiceMock()
        {
            _mockCategories = new List<CategoryDTO>
            {
                new CategoryDTO
                {
                    ID = 1,
                    Name = "Fruits"
                },
                new CategoryDTO
                {
                    ID = 2,
                    Name = "Vegetables"
                }
            };

            _mockBrands = new List<BrandDTO>
            {
                new BrandDTO
                {
                    ID = 1,
                    Name = "Pablo's Farms"
                },
                new BrandDTO
                {
                    ID = 2,
                    Name = "Marge's Farms"
                }
            };

            _mockProducts = new List<ProductDTO>
            {
                new ProductDTO
                {
                    ID = 1,
                    Name = "Banana",
                    Description = "Amazingly Tasty Bananas!",
                    Price = 1.50m,
                    Quantity = 16,
                    Category = _mockCategories.ElementAt(0),
                    Brand = _mockBrands.ElementAt(0)
                },
                new ProductDTO
                {
                    ID = 2,
                    Name = "Orange",
                    Description = "Amazingly Tasty Oranges!",
                    Price = 2.09m,
                    Quantity = 0,
                    Category = _mockCategories.ElementAt(0),
                    Brand = _mockBrands.ElementAt(0)
                },
                new ProductDTO
                {
                    ID = 3,
                    Name = "Banana",
                    Price = 3.49m,
                    Quantity = 4,
                    Description = "Amazingly Nasty Bananas!",
                    Category = _mockCategories.ElementAt(0),
                    Brand = _mockBrands.ElementAt(1)
                },
                new ProductDTO
                {
                    ID = 4,
                    Name = "Broccoli",
                    Price = 2.99m,
                    Quantity = 526,
                    Description = "Tastes like a mini tree!",
                    Category = _mockCategories.ElementAt(1),
                    Brand = _mockBrands.ElementAt(1)
                }
            };
        }

        #region methods

        public async Task<IEnumerable<ProductDTO>> GetProductCollection(string searchString = null, int? categoryID = null, int? brandID = null)
        {
            return _mockProducts.Where(product => string.IsNullOrWhiteSpace(searchString) || product.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase))
                                .Where(product => string.IsNullOrWhiteSpace(searchString) || product.Description.Contains(searchString, StringComparison.CurrentCultureIgnoreCase))
                                .Where(product => categoryID == null || product.Category.ID == categoryID)
                                .Where(product => brandID == null || product.Brand.ID == brandID);
        }

        public async Task<ProductDTO> GetProduct(int id)
        {
            return _mockProducts.Single(product => product.ID == id);
        }

        #endregion
    }
}
