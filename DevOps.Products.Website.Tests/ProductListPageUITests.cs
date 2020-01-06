using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Models.ViewModels;
using DevOps.Products.Website.Pages;
using DevOps.Products.Website.Services.Fakes.Facades;
using DevOps.Products.Website.Services.Interfaces.Facades;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DevOps.Products.Website.Tests
{
    public class ProductListPageUITests
    {
        private TestHost _testHost = new TestHost();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
                configuration.CreateMap<BrandDTO, BrandViewModel>().ReverseMap();
                configuration.CreateMap<ProductDTO, Models.ViewModels.ProductList.ProductViewModel>().ReverseMap();
            });

            _testHost.ConfigureServices(services =>
            {
                services.AddLogging();
                services.AddAuthorization();
                services.AddSingleton<IMapper>(new Mapper(mapperConfiguration));
                services.AddSingleton<IProductFacadeService, FakeProductFacadeService>();
                services.AddSingleton<ICategoryFacadeService, FakeCategoryFacadeService>();
                services.AddSingleton<IBrandFacadeService, FakeBrandFacadeService>();
            });
        }

        [Test]
        public void ProductListPage_WhenExecuted_ProductListPageLoads()
        {
            ParameterView parameterView = AuthenticationStateService.PageParametersCreator();

            RenderedComponent<ProductListWrapperTestComponent> component = _testHost.AddComponent<ProductListWrapperTestComponent>(parameterView);

            IEnumerable<HtmlNode> tableRows = component.FindAll("tr").Where(row => row.Id.Contains("ProductRow"));

            Assert.IsTrue(tableRows.Any());

            Assert.AreEqual("Products", component.Find("h1").InnerText);
        }

        [Test]
        public void ProductListPage_SearchInputGaveString_ProductListOnlyShowsRelevantRows([Values("Orange", "Banana", "Broccoli")] string searchString)
        {
            ParameterView parameterView = AuthenticationStateService.PageParametersCreator();

            RenderedComponent<ProductListWrapperTestComponent> component = _testHost.AddComponent<ProductListWrapperTestComponent>(parameterView);

            component.Find("#SearchStringInput").Input(searchString);

            IEnumerable<HtmlNode> tableNameCells = component.FindAll("#ProductRowName");

            Assert.IsTrue(tableNameCells.All(tableNameCell => tableNameCell.InnerText.Contains(searchString)));
        }

        [Test, Sequential]
        public void ProductListPage_CategorySelectValueChosen_ProductListOnlyShowsRelevantRows([Values("1", "2")] string categoryID, [Values("Fruit", "Vegetables")] string categoryName)
        {
            ParameterView parameterView = AuthenticationStateService.PageParametersCreator();

            RenderedComponent<ProductListWrapperTestComponent> component = _testHost.AddComponent<ProductListWrapperTestComponent>(parameterView);

            component.Find("#CategorySelect").Change(categoryID);

            IEnumerable<HtmlNode> tableNameCells = component.FindAll("#ProductRowCategory");

            Assert.IsTrue(tableNameCells.All(tableNameCell => tableNameCell.InnerText.Contains(categoryName)));
        }

        [Test, Sequential]
        public void ProductListPage_BrandSelectValueChosen_ProductListOnlyShowsRelevantRows([Values("1", "2")] string brandID, [Values("Pablo&#x27;s Farm", "Marge&#x27;s Farm")] string brandName)
        {
            ParameterView parameterView = AuthenticationStateService.PageParametersCreator();

            RenderedComponent<ProductListWrapperTestComponent> component = _testHost.AddComponent<ProductListWrapperTestComponent>(parameterView);

            component.Find("#BrandSelect").Change(brandID);

            IEnumerable<HtmlNode> tableNameCells = component.FindAll("#ProductRowBrand");

            Assert.IsTrue(tableNameCells.All(tableNameCell => tableNameCell.InnerText.Contains(brandName)));
        }

        [Test]
        public void ProductListPage_NotAuthenticated_CantSeeStock()
        {
            ParameterView parameterView = AuthenticationStateService.PageParametersCreator();

            RenderedComponent<ProductListWrapperTestComponent> component = _testHost.AddComponent<ProductListWrapperTestComponent>(parameterView);

            HtmlNode tableHeader = component.Find("#ProductTableHeaderQuantity");

            Assert.IsNull(tableHeader);

            IEnumerable<HtmlNode> tableNameCells = component.FindAll("#ProductRowQuantity");

            Assert.IsFalse(tableNameCells.Any());
        }

        [Test]
        public void ProductListPage_AuthenticatedAsCustomer_CanSeeStockText()
        {
            ParameterView parameterView = AuthenticationStateService.PageParametersCreator(null, new Claim(ClaimTypes.Role, "Customer"));

            RenderedComponent<ProductListWrapperTestComponent> component = _testHost.AddComponent<ProductListWrapperTestComponent>(parameterView);

            HtmlNode tableHeader = component.Find("#ProductTableHeaderQuantity");

            Assert.IsNotNull(tableHeader);

            IEnumerable<HtmlNode> tableNameCells = component.FindAll("#ProductRowQuantity");

            Assert.IsTrue(tableNameCells.All(tableNameCell => tableNameCell.InnerText.Contains("Stock")));
        }

        [Test]
        public void ProductListPage_AuthenticatedAsStaff_CanSeeStockQuantity()
        {
            ParameterView parameterView = AuthenticationStateService.PageParametersCreator(null, new Claim(ClaimTypes.Role, "Staff"));

            RenderedComponent<ProductListWrapperTestComponent> component = _testHost.AddComponent<ProductListWrapperTestComponent>(parameterView);

            HtmlNode tableHeader = component.Find("#ProductTableHeaderQuantity");

            Assert.IsNotNull(tableHeader);

            IEnumerable<HtmlNode> tableNameCells = component.FindAll("#ProductRowQuantity");

            Assert.IsTrue(tableNameCells.All(tableNameCell => tableNameCell.InnerText.All(char.IsDigit)));
        }

        public class ProductListWrapperTestComponent : ComponentBase
        {
            [Parameter] public Task<AuthenticationState> AuthenticationState { get; set; }

            protected override void BuildRenderTree(RenderTreeBuilder builder)
            {
                builder.OpenComponent<CascadingValue<Task<AuthenticationState>>>(0);
                builder.AddAttribute(1, nameof(CascadingValue<Task<AuthenticationState>>.Value), AuthenticationState);
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(builder =>
                {
                    builder.OpenComponent<ProductList>(0);
                    builder.CloseComponent();
                }));
                builder.CloseComponent();
            }
        }
    }
}
