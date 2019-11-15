using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
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
                    CategoryID = _mockCategories.ElementAt(0).ID,
                    CategoryName = _mockCategories.ElementAt(0).Name,
                    BrandID = _mockBrands.ElementAt(0).ID,
                    BrandName = _mockBrands.ElementAt(0).Name
                },
                new ProductDTO
                {
                    ID = 2,
                    Name = "Orange",
                    Description = "Amazingly Tasty Oranges!",
                    Price = 2.09m,
                    Quantity = 0,
                    CategoryID = _mockCategories.ElementAt(0).ID,
                    CategoryName = _mockCategories.ElementAt(0).Name,
                    BrandID = _mockBrands.ElementAt(0).ID,
                    BrandName = _mockBrands.ElementAt(0).Name
                },
                new ProductDTO
                {
                    ID = 3,
                    Name = "Banana",
                    Price = 3.49m,
                    Quantity = 4,
                    Description = "Amazingly Nasty Bananas!",
                    CategoryID = _mockCategories.ElementAt(0).ID,
                    CategoryName = _mockCategories.ElementAt(0).Name,
                    BrandID = _mockBrands.ElementAt(1).ID,
                    BrandName = _mockBrands.ElementAt(1).Name
                },
                new ProductDTO
                {
                    ID = 4,
                    Name = "Broccoli",
                    Price = 2.99m,
                    Quantity = 526,
                    Description = "Tastes like a mini tree!",
                    CategoryID = _mockCategories.ElementAt(1).ID,
                    CategoryName = _mockCategories.ElementAt(1).Name,
                    BrandID = _mockBrands.ElementAt(1).ID,
                    BrandName = _mockBrands.ElementAt(1).Name
                }
            };
        }

        #region methods

        public async Task<IEnumerable<ProductDTO>> GetProductCollection(string searchString = null, int? categoryID = null, int? brandID = null)
        {
            return _mockProducts.Where(product => string.IsNullOrWhiteSpace(searchString) || 
                                                  product.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase) || 
                                                  product.Description.Contains(searchString, StringComparison.CurrentCultureIgnoreCase))
                                                  .Where(product => !categoryID.HasValue || product.CategoryID == categoryID)
                                                  .Where(product => !brandID.HasValue || product.BrandID == brandID);
        }

        public async Task<ProductDTO> GetProduct(int id)
        {
            return _mockProducts.Single(product => product.ID == id);
        }

        #endregion
    }
}
