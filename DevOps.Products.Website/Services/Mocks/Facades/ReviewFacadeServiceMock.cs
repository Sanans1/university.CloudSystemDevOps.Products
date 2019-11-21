using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Services.Interfaces;
using DevOps.Products.Website.Services.Interfaces.Facades;

namespace DevOps.Products.Website.Services.Mocks.Facades
{
    public class ReviewFacadeServiceMock : IReviewFacadeService
    {

        private readonly ICollection<ReviewDTO> _mockReviews;

        public ReviewFacadeServiceMock()
        {
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
            return _mockReviews.Where(review => review.ProductID == id);
        }

        public async Task<bool> CreateReview(ReviewDTO reviewDTO)
        {
            _mockReviews.Add(reviewDTO);
            return true;
        }

        public async Task DeleteReview(int id)
        {
            _mockReviews.Remove(_mockReviews.Single(review => review.ID == id));
        }
    }
}
