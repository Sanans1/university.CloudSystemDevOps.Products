using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Composer.API.Models;
using DevOps.Products.Composer.API.Services.Interfaces.Facades;

namespace DevOps.Products.Composer.API.Services.Mocks.Facades
{
    public class BrandFacadeServiceMock : IBrandFacadeService
    {
        private readonly IEnumerable<BrandDTO> _mockBrands;

        public BrandFacadeServiceMock()
        {
            _mockBrands = new List<BrandDTO>
            {
                new BrandDTO
                {
                    ID = 1,
                    Name = "Pablo's Farms"
                },
                new BrandDTO
                {
                    ID = 2,
                    Name = "Marge's Farms"
                }
            };
        }

        public async Task<BrandDTO> GetBrand(int id)
        {
            return _mockBrands.Single(brand => brand.ID == id);
        }
    }
}
