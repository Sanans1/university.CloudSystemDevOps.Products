using System;
using AutoMapper;
using DevOps.Products.Products.DAL;
using DevOps.Products.Products.REST.API.Models;

namespace DevOps.Products.Products.REST.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap().ForMember(destination => destination.Category, options => options.Ignore())
                                                         .ForMember(destination => destination.Brand, options => options.Ignore())
                                                         .ForMember(destination => destination.PriceHistories, options => options.Ignore());

            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<Brand, BrandDTO>().ReverseMap();
             
            CreateMap<PriceHistory, PriceHistoryDTO>().ReverseMap();

            CreateMap<ProductDTO, CategoryDTO>().ForMember(destination => destination.ID, options => options.Ignore())
                                                .ForMember(destination => destination.Name, options => options.MapFrom(source => source.CategoryName));

            CreateMap<ProductDTO, BrandDTO>().ForMember(destination => destination.ID, options => options.Ignore())
                                             .ForMember(destination => destination.Name, options => options.MapFrom(source => source.BrandName));

            CreateMap<ProductDTO, PriceHistoryDTO>().ForMember(destination => destination.ID, options => options.Ignore())
                                                    .ForMember(destination => destination.DateArchived, options => options.MapFrom(source => DateTime.Now))
                                                    .ForMember(destination => destination.ProductID, options => options.MapFrom(source => source.ID));

        }
    }
}
