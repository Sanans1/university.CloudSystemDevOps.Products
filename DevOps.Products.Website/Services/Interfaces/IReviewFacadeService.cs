using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Models.ViewModels;

namespace DevOps.Products.Website.Services.Interfaces
{
    public interface IReviewFacadeService
    {
        Task<IEnumerable<ReviewDTO>> GetReviewCollection(int id);

        Task<bool> CreateReview(ReviewDTO reviewDTO);

        Task DeleteReview(int id);
    }
}