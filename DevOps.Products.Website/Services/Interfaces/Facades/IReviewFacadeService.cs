using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.DTOs;

namespace DevOps.Products.Website.Services.Interfaces.Facades
{
    public interface IReviewFacadeService
    {
        Task<IEnumerable<ReviewDTO>> GetReviewCollection(int id);

        Task<bool> CreateReview(ReviewDTO reviewDTO);

        Task DeleteReview(int id);
    }
}