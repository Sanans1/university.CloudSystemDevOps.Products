using AutoMapper;
using DevOps.Products.Reviews.DAL;
using DevOps.Products.Reviews.REST.API.Models;

namespace DevOps.Products.Reviews.REST.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Review, ReviewDTO>().ReverseMap();
        }
    }
}
