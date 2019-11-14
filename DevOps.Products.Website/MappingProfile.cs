using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevOps.Products.DTOs;
using DevOps.Products.Website.ViewModels;

namespace DevOps.Products.Website
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
            CreateMap<BrandDTO, BrandViewModel>().ReverseMap();

            CreateMap<ProductDTO, ProductDetailsViewModel>().ForMember(destination => destination.InStock, options => options.MapFrom(source => source.Quantity > 0))
                                                     .ReverseMap();

            CreateMap<CustomerDTO, CustomerViewModel>().ReverseMap();

            CreateMap<ReviewDTO, ReviewViewModel>().ForPath(destination => destination.Customer.ID, options => options.MapFrom(source => source.CustomerID))
                                                   .ReverseMap();
        }
    }
}
