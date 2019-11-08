using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Services.Interfaces;

namespace DevOps.Products.Website.Services.Implementations
{
    public class ReviewFacadeService
    {
        public async Task<IEnumerable<ProductDTO>> GetReviewCollection(int id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateReview()//TODO Create a review DTO? Maybe just take string as parameter
        {
            throw new NotImplementedException();
        }

        public async Task DeleteReview(int id)
        {
            throw new NotImplementedException();
        }
    }
}
