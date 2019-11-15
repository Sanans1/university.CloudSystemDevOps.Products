using AutoMapper;
using DevOps.Products.Products.DAL;
using DevOps.Products.Products.REST.API.Models;

namespace DevOps.Products.Products.REST.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
