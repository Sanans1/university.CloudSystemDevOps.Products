using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.DTOs;
using DevOps.Products.Website.Services.Interfaces;
using DevOps.Products.Website.Services.Interfaces.Facades;

namespace DevOps.Products.Website.Services.Implementations.Facades
{
    public class CategoryFacadeService : ICategoryFacadeService
    {
        public async Task<IEnumerable<CategoryDTO>> GetCategoryCollection()
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDTO> GetCategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}
