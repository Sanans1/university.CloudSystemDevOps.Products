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
    public class BrandRepository : GenericRepository<ProductContext, Brand, BrandDTO>
    {
        public BrandRepository(ProductContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
