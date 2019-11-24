using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Models.ViewModels;
using DevOps.Products.Website.Models.ViewModels.ProductDetails;
using DevOps.Products.Website.Services.Fakes.Facades;
using DevOps.Products.Website.Services.Implementations.Facades;
using DevOps.Products.Website.Services.Implementations.Pages;
using DevOps.Products.Website.Services.Interfaces.Facades;
using DevOps.Products.Website.States;
using Microsoft.AspNetCore.Razor.Language;
using Newtonsoft.Json;
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
                configuration.CreateMap<ProductDTO, ProductViewModel>().ForMember(destination => destination.InStock, options => options.MapFrom(source => source.Quantity > 0))
                    .ReverseMap();
            });

            _mapper = new Mapper(mapperConfiguration);
        }

        [SetUp]
        public void Setup()
        {
            _productDetailsService = new ProductDetailsService(_mapper, new FakeProductFacadeService(), new FakeReviewFacadeService(), new FakeCustomerFacadeService());
        }

        [Test]
        public async Task GetProductDetailsViewModelAsync_GetsValuesFromFacadesAndConstructsState_CorrectlyPopulatesState()
        {
            ProductViewModel expectedProductViewModel = new ProductViewModel
            {
                ID = 1,
                Name = "Banana",
                Description = "Amazingly Tasty Bananas!",
                Price = 1.50m,
                InStock = true,
                CategoryID = 1,
                CategoryName = "Fruits",
                BrandID = 1,
                BrandName = "Pablo's Farms"
            };

            CustomerViewModel expectedCustomerViewModel = new CustomerViewModel
            {
                ID = 1,
                Name = "Lewis"
            };

            ICollection<ReviewViewModel> expectedReviews = new List<ReviewViewModel>
            {
                new ReviewViewModel()
                {
                    ID = 1,
                    Rating = 3,
                    Text = "It was alright...",
                    ProductID = 1,
                    Customer = new CustomerViewModel
                    {
                        ID = 2,
                        Name = "Sarah"
                    }
                },
                new ReviewViewModel()
                {
                    ID = 2,
                    Rating = 5,
                    Text = "I loved it!",
                    ProductID = 1,
                    Customer = new CustomerViewModel
                    {
                        ID = 3,
                        Name = "Sven"
                    }
                }
            };

            ProductDetailsState expectedProductDetailsState = new ProductDetailsState(expectedProductViewModel, expectedReviews, expectedCustomerViewModel);

            string expectedSerializedProductDetailsState = JsonConvert.SerializeObject(expectedProductDetailsState);

            ProductDetailsState actualProductDetailsState = await _productDetailsService.GetProductDetailsViewModelAsync(1);

            string actualSerializedProductDetailsState = JsonConvert.SerializeObject(actualProductDetailsState);

            Assert.AreEqual(expectedSerializedProductDetailsState,actualSerializedProductDetailsState);
        }

        [Test]
        public async Task SubmitReview_SubmitsValidReview_SuccessfullySubmitsReview()
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
