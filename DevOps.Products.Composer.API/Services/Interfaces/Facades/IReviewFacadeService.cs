using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.Composer.API.Models;

namespace DevOps.Products.Composer.API.Services.Interfaces.Facades
{
    public interface IReviewFacadeService
    {
        Task<IEnumerable<ReviewDTO>> GetReviewCollection(int id);
    }
}