using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.Composer.API.Models;

namespace DevOps.Products.Composer.API.Services.Interfaces.Facades
{
    public interface IProductFacadeService
    {
        Task<IEnumerable<ProductDTO>> GetProductCollection(string searchString = null, int? categoryID = null, int? brandID = null);
        Task<ProductDTO> GetProduct(int id);
    }
}