using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Models.ViewModels;
using DevOps.Products.Website.Services.Interfaces;

namespace DevOps.Products.Website.Services.Implementations
{
    public class ReviewFacadeService : IReviewFacadeService
    {
        public async Task<IEnumerable<ReviewDTO>> GetReviewCollection(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateReview(ReviewDTO reviewDTO)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteReview(int id)
        {
            throw new NotImplementedException();
        }
    }
}
