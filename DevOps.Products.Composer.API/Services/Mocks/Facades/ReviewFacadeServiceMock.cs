using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Composer.API.Models;
using DevOps.Products.Composer.API.Services.Interfaces.Facades;

namespace DevOps.Products.Composer.API.Services.Mocks.Facades
{
    public class ReviewFacadeServiceMock : IReviewFacadeService
    {
        private readonly ICustomerFacadeService _customerFacadeService;

        private readonly ICollection<ReviewDTO> _mockReviews;

        public ReviewFacadeServiceMock(ICustomerFacadeService customerFacadeService)
        {
            _customerFacadeService = customerFacadeService;

            _mockReviews = new List<ReviewDTO>
            {
                new ReviewDTO()
                {
                    ID = 1,
                    Rating = 3,
                    Text = "It was alright...",
                    ProductID = 1,
                    CustomerID = 2
                },
                new ReviewDTO()
                {
                    ID = 2,
                    Rating = 5,
                    Text = "I loved it!",
                    ProductID = 1,
                    CustomerID = 3
                }
            };
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewCollection(int id)
        {
            IEnumerable<ReviewDTO> reviews =  _mockReviews.Where(review => review.ProductID == id);

            foreach (ReviewDTO review in reviews)
            {
                CustomerDTO customer = await _customerFacadeService.GetCustomer(review.CustomerID);

                review.CustomerName = customer.Name;
            }

            return reviews;
        }
    }
}
