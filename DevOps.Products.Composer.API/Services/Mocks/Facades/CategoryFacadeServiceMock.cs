using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Composer.API.Models;
using DevOps.Products.Composer.API.Services.Interfaces.Facades;

namespace DevOps.Products.Composer.API.Services.Mocks.Facades
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

        public async Task<CategoryDTO> GetCategory(int id)
        {
            return _mockCategories.Single(category => category.ID == id);
        }
    }
}
