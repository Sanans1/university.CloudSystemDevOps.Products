using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using DevOps.Products.Common.Repository;
using DevOps.Products.Products.DAL;
using DevOps.Products.Products.REST.API.Models;

namespace DevOps.Products.Products.REST.API.Services.Repos
{
    public class ProductRepository : GenericRepository<ProductContext, Product, ProductDTO>
    {
        public ProductRepository(ProductContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
