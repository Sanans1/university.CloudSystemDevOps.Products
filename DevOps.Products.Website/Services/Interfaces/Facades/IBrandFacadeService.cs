using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.DTOs;

namespace DevOps.Products.Website.Services.Interfaces.Facades
{
    public interface IBrandFacadeService
    {
        Task<IEnumerable<BrandDTO>> GetBrandCollection();
        Task<BrandDTO> GetBrand(int id);
    }
}