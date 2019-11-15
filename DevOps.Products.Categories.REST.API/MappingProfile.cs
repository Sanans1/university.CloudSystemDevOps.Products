using AutoMapper;
using DevOps.Products.Categories.DAL;
using DevOps.Products.Categories.REST.API.Models;

namespace DevOps.Products.Categories.REST.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
