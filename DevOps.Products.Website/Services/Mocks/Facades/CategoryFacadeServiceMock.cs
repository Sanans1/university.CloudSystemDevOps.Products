using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.DTOs;
using DevOps.Products.Website.Services.Interfaces.Facades;

namespace DevOps.Products.Website.Services.Mocks.Facades
{
    public class CategoryFacadeServiceMock : ICategoryFacadeService
    {
        private readonly IEnumerable<CategoryDTO> _mockCategories;

        public CategoryFacadeServiceMock()
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

        public async Task<CategoryDTO> GetCategory(int id)
        {
            return _mockCategories.Single(category => category.ID == id);
        }
    }
}
