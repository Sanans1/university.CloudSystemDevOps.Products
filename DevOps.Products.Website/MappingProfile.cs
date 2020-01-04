using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Models.ViewModels;

namespace DevOps.Products.Website
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
            CreateMap<BrandDTO, BrandViewModel>().ReverseMap();

            CreateMap<ProductDTO, Models.ViewModels.ProductDetails.ProductViewModel>().ReverseMap();

            CreateMap<ProductDTO, Models.ViewModels.ProductList.ProductViewModel>().ReverseMap();

            CreateMap<CustomerDTO, CustomerViewModel>().ReverseMap();

            CreateMap<ReviewDTO, ReviewViewModel>().ReverseMap();

            CreateMap<OrderDTO, OrderViewModel>().ReverseMap();
        }
    }
}
