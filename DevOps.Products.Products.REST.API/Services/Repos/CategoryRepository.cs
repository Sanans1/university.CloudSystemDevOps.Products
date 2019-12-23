using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevOps.Products.Common.Repository;
using DevOps.Products.Products.DAL;
using DevOps.Products.Products.REST.API.Models;

namespace DevOps.Products.Products.REST.API.Services.Repos
{
    public class CategoryRepository : GenericRepository<ProductContext, Category, CategoryDTO>
    {
        public CategoryRepository(ProductContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
