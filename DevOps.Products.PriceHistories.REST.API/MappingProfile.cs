using AutoMapper;
using DevOps.Products.PriceHistories.DAL;
using DevOps.Products.PriceHistories.REST.API.Models;

namespace DevOps.Products.PriceHistories.REST.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PriceHistory, PriceHistoryDTO>().ReverseMap();
        }
    }
}
