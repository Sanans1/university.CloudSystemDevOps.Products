using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DevOps.Products.DTOs;
using DevOps.Products.Website.Models.ViewModels;
using DevOps.Products.Website.Models.ViewModels.Pages;
using DevOps.Products.Website.Services.Interfaces;
using DevOps.Products.Website.Services.Interfaces.Facades;
using DevOps.Products.Website.Services.Interfaces.Pages;

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

        public async Task<ProductDetailsViewModel> GetProductDetailsViewModelAsync(int id)
        {
            ProductViewModel product = _mapper.Map<ProductViewModel>(await _productFacadeService.GetProduct(id));
            IEnumerable<ReviewViewModel> reviews = _mapper.Map<IEnumerable<ReviewViewModel>>(await _reviewFacadeService.GetReviewCollection(id));
            CustomerViewModel customer = _mapper.Map<CustomerViewModel>(await _customerFacadeService.GetCurrentCustomer());

            foreach (ReviewViewModel review in reviews)
            {
                review.Customer = _mapper.Map<CustomerViewModel>(await _customerFacadeService.GetCustomer(review.Customer.ID));
            }

            return new ProductDetailsViewModel(product, reviews, customer);
        }

        public async Task<bool> SubmitReview(ReviewViewModel review)
        {
            ReviewDTO reviewDTO = _mapper.Map<ReviewDTO>(review);

            return await _reviewFacadeService.CreateReview(reviewDTO);
        }

        #endregion

    }
}
