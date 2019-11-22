using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Models.ViewModels;
using DevOps.Products.Website.Services.Implementations.Facades;
using DevOps.Products.Website.Services.Implementations.Pages;
using DevOps.Products.Website.Services.Interfaces.Facades;
using DevOps.Products.Website.Services.Mocks.Facades;
using DevOps.Products.Website.States;
using Microsoft.AspNetCore.Razor.Language;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DevOps.Products.Website.Tests
{
    [TestFixture]
    public class ProductDetailsServiceTests
    {
        private IMapper _mapper;
        private ProductDetailsService _productDetailsService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<CustomerDTO, CustomerViewModel>().ReverseMap();
                configuration.CreateMap<ReviewDTO, ReviewViewModel>().ForPath(destination => destination.Customer.ID, options => options.MapFrom(source => source.CustomerID))
                    .ReverseMap();
                configuration.CreateMap<ProductDTO, Models.ViewModels.ProductDetails.ProductViewModel>().ForMember(destination => destination.InStock, options => options.MapFrom(source => source.Quantity > 0))
                    .ReverseMap();
            });

            _mapper = new Mapper(mapperConfiguration);
        }

        [SetUp]
        public void Setup()
        {
            _productDetailsService = new ProductDetailsService(_mapper, new ProductFacadeServiceMock(), new ReviewFacadeServiceMock(), new CustomerFacadeServiceMock());
        }

        [Test]
        public async Task ProductDetailsService_CorrectlyPopulatesState([Range(1,4,1)]int productID)
        {
            ProductDetailsState productDetailsState = await _productDetailsService.GetProductDetailsViewModelAsync(productID);

            Assert.Multiple(() =>
            {
                Assert.NotNull(productDetailsState.Product);
                Assert.NotNull(productDetailsState.Customer);
                Assert.NotNull(productDetailsState.ReviewForm);
            });
        }

        [Test]
        public async Task ProductDetailsService_SuccessfullySubmitsReview()
        {
            CustomerViewModel customer = new CustomerViewModel
            {
                ID = 0,
                Name = "TestCustomer"
            };

            ReviewViewModel expectedReview = new ReviewViewModel
            {
                ID = 0,
                Rating = 3,
                Text = "This is a test review.",
                ProductID = 1,
                Customer = customer
            };

            ReviewViewModel actualReview = await _productDetailsService.SubmitReview(expectedReview);

            Assert.AreEqual(expectedReview, actualReview);
        }
    }
}
