using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Services.Interfaces;
using DevOps.Products.Website.Services.Interfaces.Facades;

namespace DevOps.Products.Website.Services.Implementations.Facades
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
