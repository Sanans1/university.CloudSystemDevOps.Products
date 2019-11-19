using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Models.ViewModels;
using DevOps.Products.Website.Models.ViewModels.ProductDetails;
using DevOps.Products.Website.Services.Interfaces;
using DevOps.Products.Website.Services.Interfaces.Facades;
using DevOps.Products.Website.Services.Interfaces.Pages;
using DevOps.Products.Website.States;

namespace DevOps.Products.Website.Services.Implementations.Pages
{
    public class ProductDetailsService : IProductDetailsService
    {
        #region fields

        private readonly IMapper _mapper;
        private readonly IProductFacadeService _productFacadeService;
        private readonly IReviewFacadeService _reviewFacadeService;
        private readonly ICustomerFacadeService _customerFacadeService;

        #endregion

        public ProductDetailsService(IMapper mapper, IProductFacadeService productFacadeService, IReviewFacadeService reviewFacadeService, ICustomerFacadeService customerFacadeService)
        {
            _mapper = mapper;
            _productFacadeService = productFacadeService;
            _reviewFacadeService = reviewFacadeService;
            _customerFacadeService = customerFacadeService;
        }

        #region methods

        public async Task<ProductDetailsState> GetProductDetailsViewModelAsync(int id)//TODO extract this out into maybe two microservices, one for constructing products and another for reviews?
        {
            CustomerViewModel customer = _mapper.Map<CustomerViewModel>(await _customerFacadeService.GetCurrentCustomer());

            ProductViewModel productDetails = _mapper.Map<ProductViewModel>(await _productFacadeService.GetProduct(id));

            ICollection<ReviewViewModel> reviews = _mapper.Map<ICollection<ReviewViewModel>>(await _reviewFacadeService.GetReviewCollection(id));

            foreach (ReviewViewModel review in reviews)
            {
                review.Customer = _mapper.Map<CustomerViewModel>(await _customerFacadeService.GetCustomer(review.Customer.ID));
            }

            return new ProductDetailsState(productDetails, reviews, customer);
        }

        public async Task<ReviewViewModel> SubmitReview(ReviewViewModel review)
        {
            ReviewDTO reviewDTO = _mapper.Map<ReviewDTO>(review);

            return _mapper.Map<ReviewViewModel>(await _reviewFacadeService.CreateReview(reviewDTO));
        }

        #endregion

    }
}
