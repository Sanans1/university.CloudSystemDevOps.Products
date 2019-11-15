using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.Composer.API.Models;
using DevOps.Products.Composer.API.Services.Interfaces.Facades;

namespace DevOps.Products.Composer.API.Services.Implementations.Facades
{
    public class ProductFacadeService : IProductFacadeService
    {
        public async Task<IEnumerable<ProductDTO>> GetProductCollection(string searchString = null, int? categoryID = null, int? brandID = null)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDTO> GetProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
