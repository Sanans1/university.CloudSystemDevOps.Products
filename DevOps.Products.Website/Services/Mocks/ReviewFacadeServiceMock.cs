using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Models.ViewModels;
using DevOps.Products.Website.Services.Interfaces;

namespace DevOps.Products.Website.Services.Mocks
{
    public class ReviewFacadeServiceMock : IReviewFacadeService
    {

        private readonly ICollection<ReviewDTO> _mockReviewDTOs;

        public ReviewFacadeServiceMock()
        {
            _mockReviewDTOs = new List<ReviewDTO>()
            {
                new ReviewDTO()
                {
                    ID = 1,
                    CustomerName = "Richard",
                    Rating = 3,
                    Text = "It was alright...",
                    ProductID = 1
                },
                new ReviewDTO()
                {
                    ID = 2,
                    CustomerName = "Mary",
                    Rating = 5,
                    Text = "I loved it!",
                    ProductID = 1
                }
            };
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewCollection(int id)
        {
            return _mockReviewDTOs.Where(review => review.ProductID == id);
        }

        public async Task CreateReview(ReviewDTO reviewDTO)
        {
            _mockReviewDTOs.Add(reviewDTO);
        }

        public async Task DeleteReview(int id)
        {
            _mockReviewDTOs.Remove(_mockReviewDTOs.Single(review => review.ID == id));
        }
    }
}
