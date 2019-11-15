using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.Composer.API.Models;
using DevOps.Products.Composer.API.Services.Interfaces.Facades;

namespace DevOps.Products.Composer.API.Services.Implementations.Facades
{
    public class ReviewFacadeService : IReviewFacadeService
    {
        public async Task<IEnumerable<ReviewDTO>> GetReviewCollection(int id)
        {
            throw new NotImplementedException();
        }
    }
}
