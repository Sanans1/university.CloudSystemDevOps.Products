using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Models.ViewModels;
using DevOps.Products.Website.Services.Interfaces;

namespace DevOps.Products.Website.Services.Implementations
{
    public class ProductDetailsService : IProductDetailsService
    {

        #region fields

        private readonly IProductFacadeService _productFacadeService;

        #endregion

        public ProductDetailsService(IProductFacadeService productFacadeService)
        {
            _productFacadeService = productFacadeService;
        }

        #region methods

        public async Task<ProductDetailsViewModel> GetProductDetailsViewModelAsync(int id)
        {
            ProductDTO product = await _productFacadeService.GetProduct(id);

            return new ProductDetailsViewModel(product);
        }

        #endregion

    }
}
