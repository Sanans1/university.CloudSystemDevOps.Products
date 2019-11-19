using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Services.Interfaces.Facades;

namespace DevOps.Products.Website.Services.Mocks.Facades
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

        public async Task<IEnumerable<BrandDTO>> GetBrandCollection()
        {
            return _mockBrands;
        }
    }
}
