using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Services.Interfaces.Facades;

namespace DevOps.Products.Website.Services.Fakes.Facades
{
    public class FakeCategoryFacadeService : ICategoryFacadeService
    {
        private readonly IEnumerable<CategoryDTO> _mockCategories;

        public FakeCategoryFacadeService()
        {
            _mockCategories = new List<CategoryDTO>
            {
                new CategoryDTO
                {
                    ID = 1,
                    Name = "Fruits"
                },
                new CategoryDTO
                {
                    ID = 2,
                    Name = "Vegetables"
                }
            };
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoryCollection()
        {
            return _mockCategories;
        }
    }
}
