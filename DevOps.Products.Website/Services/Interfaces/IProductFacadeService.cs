using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;

namespace DevOps.Products.Website.Services.Interfaces
{
    public interface IProductFacadeService
    {
        Task<IEnumerable<ProductDTO>> GetProductCollection(string name = null, string brand = null, string category = null);
        Task<ProductDTO> GetProduct(int id);
    }
}