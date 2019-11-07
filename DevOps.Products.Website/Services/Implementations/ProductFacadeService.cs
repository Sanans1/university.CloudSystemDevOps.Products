using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Services.Interfaces;

namespace DevOps.Products.Website.Services.Implementations
{
    public class ProductFacadeService : IProductFacadeService
    {
        public async Task<IEnumerable<ProductDTO>> GetProductCollection(string name = null, string brand = null, string category = null)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDTO> GetProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
