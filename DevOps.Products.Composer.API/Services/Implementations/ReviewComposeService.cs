using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Composer.API.Models;
using DevOps.Products.Composer.API.Services.Interfaces;

namespace DevOps.Products.Composer.API.Services.Implementations
{
    public class ReviewComposeService : IReviewComposeService
    {
        public async Task<IEnumerable<ReviewDTO>> GetReviewCollection(int id)
        {
            throw new NotImplementedException();
        }
    }
}
