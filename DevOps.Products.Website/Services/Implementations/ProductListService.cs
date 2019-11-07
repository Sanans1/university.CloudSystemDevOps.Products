using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Models.ViewModels;
using DevOps.Products.Website.Services.Interfaces;

namespace DevOps.Products.Website.Services.Implementations
{
    public class ProductListService : IProductListService
    {
        private readonly IProductFacadeService _productFacadeService;

        public ProductListService(IProductFacadeService productFacadeService)
        {
            _productFacadeService = productFacadeService;
        }

        public async Task<ProductListViewModel[]> GetProductListViewModelsAsync(string name = null, string brand = null, string category = null)
        {
            IEnumerable<ProductDTO> products = await _productFacadeService.GetProductCollection(name, brand, category);

            return products.Select(product => new ProductListViewModel(product)).ToArray();
        }
    }
}
