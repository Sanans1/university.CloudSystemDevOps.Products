using System.Threading.Tasks;
using AutoMapper;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Models.ViewModels;
using DevOps.Products.Website.Services.Implementations.Pages;
using DevOps.Products.Website.Services.Interfaces.Pages;
using DevOps.Products.Website.Services.Mocks.Facades;
using DevOps.Products.Website.States;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DevOps.Products.Website.Tests
{
    [TestFixture]
    public class ProductListServiceTests
    {
        private IMapper _mapper;
        private ProductListService _productListService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<CategoryDTO, CategoryViewModel>();
                configuration.CreateMap<BrandDTO, BrandViewModel>();
                configuration.CreateMap<ProductDTO, Models.ViewModels.ProductList.ProductViewModel>().ForMember(
                    destination => destination.InStock, options => options.MapFrom(source => source.Quantity > 0));
            });

            _mapper = new Mapper(mapperConfiguration);
        }

        [SetUp]
        public void Setup()
        {
            _productListService = new ProductListService(_mapper, new ProductFacadeServiceMock(), new CategoryFacadeServiceMock(), new BrandFacadeServiceMock());
        }

        [Test]
        public async Task ProductListService_CorrectlyPopulatesState()
        {
            ProductListState productListState = await _productListService.GetProductListViewModelsAsync();

            Assert.Multiple(() =>
            {
                CollectionAssert.IsNotEmpty(productListState.Products);
                CollectionAssert.IsNotEmpty(productListState.Categories);
                CollectionAssert.IsNotEmpty(productListState.Brands);
            });
        }
    }
}