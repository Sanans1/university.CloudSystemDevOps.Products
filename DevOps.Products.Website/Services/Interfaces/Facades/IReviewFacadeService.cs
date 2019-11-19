using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;

namespace DevOps.Products.Website.Services.Interfaces.Facades
{
    public interface IReviewFacadeService
    {
        Task<IEnumerable<ReviewDTO>> GetReviewCollection(int id);

        Task<ReviewDTO> CreateReview(ReviewDTO reviewDTO);

        Task DeleteReview(int id);
    }
}