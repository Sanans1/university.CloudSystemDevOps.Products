using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.DTOs;

namespace DevOps.Products.Website.Services.Interfaces.Facades
{
    public interface IProductFacadeService
    {
        Task<IEnumerable<ProductDTO>> GetProductCollection(string searchString = null, int? categoryID = null, int? brandID = null);
        Task<ProductDTO> GetProduct(int id);
    }
}