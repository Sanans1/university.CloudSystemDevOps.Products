using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;

namespace DevOps.Products.Website.Services.Interfaces.Facades
{
    public interface ICategoryFacadeService
    {
        Task<IEnumerable<CategoryDTO>> GetCategoryCollection();
        Task<CategoryDTO> GetCategory(int id);
    }
}