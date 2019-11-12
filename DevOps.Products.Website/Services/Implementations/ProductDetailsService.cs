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

            return new ProductDetailsViewModel(product, reviews, null/*TODO Add a Customer Facade*/);
        }

        public async Task<bool> SubmitReview(ProductDetailsViewModel productDetailsViewModel, string reviewText, int rating)
        {
            ReviewDTO reviewDTO = new ReviewDTO()
            {
                CustomerName = "Lewis",//productDetailsViewModel.CustomerViewModel.CustomerName,
                Text = reviewText,
                Rating = rating,
                ProductID = productDetailsViewModel.ID
            };

            return await _reviewFacadeService.CreateReview(reviewDTO);
        }

        #endregion

    }
}
