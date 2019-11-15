using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Composer.API.Models;
using DevOps.Products.Composer.API.Services.Interfaces.Facades;

namespace DevOps.Products.Composer.API.Services.Mocks.Facades
{
    public class ProductFacadeServiceMock : IProductFacadeService
    {

        #region fields

        private readonly ICategoryFacadeService _categoryFacadeService;
        private readonly IBrandFacadeService _brandFacadeService;
        private readonly IReviewFacadeService _reviewFacadeService;

        private readonly IEnumerable<ProductDTO> _mockProducts;

        #endregion

        public ProductFacadeServiceMock(ICategoryFacadeService categoryFacadeService, IBrandFacadeService brandFacadeService, 
                                        IReviewFacadeService reviewFacadeService)
        {
            _categoryFacadeService = categoryFacadeService;
            _brandFacadeService = brandFacadeService;
            _reviewFacadeService = reviewFacadeService;

            _mockProducts = new List<ProductDTO>
            {
                new ProductDTO
                {
                    ID = 1,
                    Name = "Banana",
                    Description = "Amazingly Tasty Bananas!",
                    Price = 1.50m,
                    Quantity = 16,
                    CategoryID = 1,
                    BrandID = 1
                },
                new ProductDTO
                {
                    ID = 2,
                    Name = "Orange",
                    Description = "Amazingly Tasty Oranges!",
                    Price = 2.09m,
                    Quantity = 0,
                    CategoryID = 1,
                    BrandID = 1
                },
                new ProductDTO
                {
                    ID = 3,
                    Name = "Banana",
                    Price = 3.49m,
                    Quantity = 4,
                    Description = "Amazingly Nasty Bananas!",
                    CategoryID = 1,
                    BrandID = 2
                },
                new ProductDTO
                {
                    ID = 4,
                    Name = "Broccoli",
                    Price = 2.99m,
                    Quantity = 526,
                    Description = "Tastes like a mini tree!",
                    CategoryID = 2,
                    BrandID = 2
                }
            };
        }

        #region methods

        public async Task<IEnumerable<ProductDTO>> GetProductCollection(string searchString = null, int? categoryID = null, int? brandID = null)
        {
            IEnumerable<ProductDTO> products = _mockProducts.Where(product => string.IsNullOrWhiteSpace(searchString) || 
                                                                              product.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase) || 
                                                                              product.Description.Contains(searchString, StringComparison.CurrentCultureIgnoreCase))
                                                                              .Where(product => !categoryID.HasValue || product.CategoryID == categoryID)
                                                                              .Where(product => !brandID.HasValue || product.BrandID == brandID);

            foreach (ProductDTO product in products)
            {
                CategoryDTO category = await _categoryFacadeService.GetCategory(product.CategoryID);

                product.CategoryName = category.Name;

                BrandDTO brand = await _brandFacadeService.GetBrand(product.BrandID);

                product.BrandName = brand.Name;
            }

            return products;
        }

        public async Task<ProductDTO> GetProduct(int id)
        {
            ProductDTO product = _mockProducts.Single(product => product.ID == id);

            CategoryDTO category = await _categoryFacadeService.GetCategory(product.CategoryID);

            product.CategoryName = category.Name;

            BrandDTO brand = await _brandFacadeService.GetBrand(product.BrandID);

            product.BrandName = brand.Name;

            product.Reviews = await _reviewFacadeService.GetReviewCollection(product.ID);

            return product;
        }

        #endregion
    }
}
