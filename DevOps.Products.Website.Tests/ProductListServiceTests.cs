using System.Threading.Tasks;
using AutoMapper;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Models.ViewModels;
using DevOps.Products.Website.Services.Fakes.Facades;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DevOps.Products.Website.Tests
{
    [TestFixture]
    public class ProductListServiceTests
    {
        //private const string IN_STOCK_STRING = "In Stock";
        //private const string OUT_OF_STOCK_STRING = "Out of Stock";

        //private IMapper _mapper;
        //private ProductListService _productListService;

        //[OneTimeSetUp]
        //public void OneTimeSetup()
        //{
        //    MapperConfiguration mapperConfiguration = new MapperConfiguration(configuration =>
        //    {
        //        configuration.CreateMap<CategoryDTO, CategoryViewModel>();
        //        configuration.CreateMap<BrandDTO, BrandViewModel>();
        //        configuration.CreateMap<ProductDTO, Models.ViewModels.ProductList.ProductViewModel>().ForMember(
        //            destination => destination.InStock,
        //            options => options.MapFrom(source => source.Quantity > 0 ? IN_STOCK_STRING : OUT_OF_STOCK_STRING));
        //    });

        //    _mapper = new Mapper(mapperConfiguration);
        //}

        //[SetUp]
        //public void Setup()
        //{
        //    _productListService = new ProductListService(_mapper, new FakeProductFacadeService(), new FakeCategoryFacadeService(), new FakeBrandFacadeService());
        //}

        //[Test]
        //public async Task GetProductListViewModelsAsync_GetsValuesFromFacadesAndConstructsState_Succeeds()
        //{
        //    ProductListState productListState = await _productListService.GetProductListViewModelsAsync();

        //    Assert.Multiple(() =>
        //    {
        //        CollectionAssert.IsNotEmpty(productListState.Products);
        //        CollectionAssert.IsNotEmpty(productListState.Categories);
        //        CollectionAssert.IsNotEmpty(productListState.Brands);
        //    });
        //}
    }
}
