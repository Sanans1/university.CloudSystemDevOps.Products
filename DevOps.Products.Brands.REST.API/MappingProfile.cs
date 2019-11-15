using AutoMapper;
using DevOps.Products.Brands.DAL;
using DevOps.Products.Brands.REST.API.Models;

namespace DevOps.Products.Brands.REST.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, BrandDTO>().ReverseMap();
        }
    }
}
