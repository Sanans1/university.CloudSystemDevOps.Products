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
        private readonly IReviewFacadeService _reviewFacadeService;

        #endregion

        public ProductDetailsService(IProductFacadeService productFacadeService, IReviewFacadeService reviewFacadeService)
        {
            _productFacadeService = productFacadeService;
            _reviewFacadeService = reviewFacadeService;
        }

        #region methods

        public async Task<ProductDetailsViewModel> GetProductDetailsViewModelAsync(int id)
        {
            ProductDTO product = await _productFacadeService.GetProduct(id);
            IEnumerable<ReviewDTO> reviews = await _reviewFacadeService.GetReviewCollection(id);

            return new ProductDetailsViewModel(product, reviews);
        }

        #endregion

    }
}
