using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.Composer.API.Models;

namespace DevOps.Products.Composer.API.Services.Interfaces
{
    public interface IReviewComposeService
    {
        Task<IEnumerable<ReviewDTO>> GetReviewCollection(int id);
    }
}