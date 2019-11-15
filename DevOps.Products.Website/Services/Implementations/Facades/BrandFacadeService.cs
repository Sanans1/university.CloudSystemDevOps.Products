using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Services.Interfaces;
using DevOps.Products.Website.Services.Interfaces.Facades;

namespace DevOps.Products.Website.Services.Implementations.Facades
{
    public class BrandFacadeService : IBrandFacadeService
    {
        public async Task<IEnumerable<BrandDTO>> GetBrandCollection()
        {
            throw new NotImplementedException();
        }

        public async Task<BrandDTO> GetBrand(int id)
        {
            throw new NotImplementedException();
        }
    }
}
