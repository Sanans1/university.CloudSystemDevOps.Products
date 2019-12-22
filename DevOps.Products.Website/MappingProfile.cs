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
        private const string IN_STOCK_STRING = "In Stock";
        private const string OUT_OF_STOCK_STRING = "Out of Stock";

        public MappingProfile()
        {
            CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
            CreateMap<BrandDTO, BrandViewModel>().ReverseMap();

            CreateMap<ProductDTO, Models.ViewModels.ProductDetails.ProductViewModel>().ForMember(destination => destination.InStock, options => options.MapFrom(source => source.Quantity > 0 ? IN_STOCK_STRING : OUT_OF_STOCK_STRING))
                .ReverseMap();

            CreateMap<ProductDTO, Models.ViewModels.ProductList.ProductViewModel>().ForMember(destination => destination.InStock, options => options.MapFrom(source => source.Quantity > 0 ? IN_STOCK_STRING : OUT_OF_STOCK_STRING))
                                                     .ReverseMap();

            CreateMap<CustomerDTO, CustomerViewModel>().ReverseMap();

            CreateMap<ReviewDTO, ReviewViewModel>().ForPath(destination => destination.Customer.ID, options => options.MapFrom(source => source.CustomerID))
                                                   .ReverseMap();
        }
    }
}
