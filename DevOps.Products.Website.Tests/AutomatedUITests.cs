using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Models.ViewModels;
using DevOps.Products.Website.Pages;
using DevOps.Products.Website.Services.Fakes.Facades;
using DevOps.Products.Website.Services.Interfaces.Facades;
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
    public class AutomatedUITests// : IDisposable
    {
        //private readonly IWebDriver _driver;
        //public AutomatedUITests()
        //{
        //    _driver = new ChromeDriver();
        //}

        //public void Dispose()
        //{
        //    _driver.Quit();
        //    _driver.Dispose();
        //}

        //[Test]
        //public async Task ProductListPage_WhenExecuted_ReturnsProductListPage()
        //{
        //    _driver.Navigate().GoToUrl("localhost:63651");

        //    IWebElement header = _driver.FindElement(By.Id("TitleHeader"));
        //    Assert.IsTrue(header.Displayed);
        //    Assert.AreEqual("Products", header.Text);
        //}

        private TestHost _host = new TestHost();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<CategoryDTO, CategoryViewModel>();
                configuration.CreateMap<BrandDTO, BrandViewModel>();
                configuration.CreateMap<ProductDTO, Models.ViewModels.ProductList.ProductViewModel>();
            });

            //_host.AddService<IMapper, Mapper>(new Mapper(mapperConfiguration));
            //_host.AddService<IProductFacadeService, FakeProductFacadeService>(new FakeProductFacadeService());
            //_host.AddService<ICategoryFacadeService, FakeCategoryFacadeService>(new FakeCategoryFacadeService());
            //_host.AddService<IBrandFacadeService, FakeBrandFacadeService>(new FakeBrandFacadeService());

            _host.ConfigureServices(services =>
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
        public async Task ProductListPage_WhenExecuted_ProductListPageLoads()
        {
            var principal = new ClaimsPrincipal(new ClaimsIdentity());

            Task<AuthenticationState> t = Task.FromResult(new AuthenticationState(principal));
            var parameterView = ParameterView.FromDictionary(new Dictionary<string, object> { ["AuthenticationState"] = t });

            RenderedComponent<ProductListWrapperTestComponent> component = _host.AddComponent<ProductListWrapperTestComponent>(parameterView);
            
            Assert.AreEqual("Products", component.Find("h1").InnerText);
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
