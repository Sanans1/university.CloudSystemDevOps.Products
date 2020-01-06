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
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DevOps.Products.Website.Tests
{
    public class ProductDetailsPageUITests
    {
        private TestHost _testHost = new TestHost();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<ProductDTO, Models.ViewModels.ProductDetails.ProductViewModel>().ReverseMap();
                configuration.CreateMap<ReviewDTO, ReviewViewModel>().ReverseMap();
                configuration.CreateMap<CustomerDTO, CustomerViewModel>().ReverseMap();
                configuration.CreateMap<OrderDTO, OrderViewModel>().ReverseMap();
            });

            _testHost.ConfigureServices(services =>
            {
                services.AddLogging();
                services.AddAuthorization();
                services.AddSingleton<IMapper>(new Mapper(mapperConfiguration));
                services.AddSingleton<IProductFacadeService, FakeProductFacadeService>();
                services.AddSingleton<IReviewFacadeService, FakeReviewFacadeService>();
                services.AddSingleton<ICustomerFacadeService, FakeCustomerFacadeService>();
                services.AddSingleton<IOrderFacadeService, FakeOrderFacadeService>();
            });
        }

        [Test, Sequential]
        public void ProductDetailsPage_WhenExecuted_ProductDetailsPageLoads([Values("1", "2", "3", "4")] string productID, [Values("Banana", "Orange", "Banana", "Broccoli")] string productName)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>() { ["ProductID"] = productID };

            ParameterView parameterView = AuthenticationStateService.PageParametersCreator(parameters);

            RenderedComponent<ProductDetailsWrapperTestComponent> component = _testHost.AddComponent<ProductDetailsWrapperTestComponent>(parameterView);

            Assert.AreEqual(productName, component.Find("h1").InnerText);
        }

        [Test]
        public void ProductDetailsPage_NotAuthenticated_CantSeeStock()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>() { ["ProductID"] = "1" };

            ParameterView parameterView = AuthenticationStateService.PageParametersCreator(parameters);

            RenderedComponent<ProductDetailsWrapperTestComponent> component = _testHost.AddComponent<ProductDetailsWrapperTestComponent>(parameterView);

            HtmlNode tableHeader = component.Find("#ProductHeaderQuantity");

            Assert.IsNull(tableHeader);

            IEnumerable<HtmlNode> tableNameCells = component.FindAll("#ProductQuantity");

            Assert.IsFalse(tableNameCells.Any());
        }

        [Test]
        public void ProductDetailsPage_AuthenticatedAsCustomer_CanSeeStockText()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>() { ["ProductID"] = "1" };

            ParameterView parameterView = AuthenticationStateService.PageParametersCreator(parameters,new Claim(ClaimTypes.Name, "Bill"), new Claim(ClaimTypes.Role, "Customer"));

            RenderedComponent<ProductDetailsWrapperTestComponent> component = _testHost.AddComponent<ProductDetailsWrapperTestComponent>(parameterView);

            HtmlNode tableHeader = component.Find("#ProductHeaderQuantity");

            Assert.IsNotNull(tableHeader);

            IEnumerable<HtmlNode> tableNameCells = component.FindAll("#ProductQuantity");

            Assert.IsTrue(tableNameCells.All(tableNameCell => tableNameCell.InnerText.Contains("Stock")));
        }

        [Test]
        public void ProductDetailsPage_AuthenticatedAsStaff_CanSeeStockQuantity()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>() { ["ProductID"] = "1" };

            ParameterView parameterView = AuthenticationStateService.PageParametersCreator(parameters, new Claim(ClaimTypes.Name, "Lewis"), new Claim(ClaimTypes.Role, "Staff"));

            RenderedComponent<ProductDetailsWrapperTestComponent> component = _testHost.AddComponent<ProductDetailsWrapperTestComponent>(parameterView);

            HtmlNode tableHeader = component.Find("#ProductHeaderQuantity");

            Assert.IsNotNull(tableHeader);

            IEnumerable<HtmlNode> tableNameCells = component.FindAll("#ProductQuantity");

            Assert.IsTrue(tableNameCells.All(tableNameCell => tableNameCell.InnerText.All(char.IsDigit)));
        }

        [Test]
        public void ProductDetailsPage_NotAuthenticated_CantSeeReviewFormOrDeleteButtons()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>() { ["ProductID"] = "1" };

            ParameterView parameterView = AuthenticationStateService.PageParametersCreator(parameters);

            RenderedComponent<ProductDetailsWrapperTestComponent> component = _testHost.AddComponent<ProductDetailsWrapperTestComponent>(parameterView);

            HtmlNode reviewForm = component.Find("#ReviewForm");

            Assert.IsNull(reviewForm);

            HtmlNode reviewDeleteButton = component.Find("#ReviewDeleteButton");

            Assert.IsNull(reviewDeleteButton);
        }

        [Test]
        public void ProductDetailsPage_CustomerReviewsProductTheyHaveBought_ReviewIsAddedToList()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>() { ["ProductID"] = "2" };

            ParameterView parameterView = AuthenticationStateService.PageParametersCreator(parameters, new Claim(ClaimTypes.Name, "Bill"), new Claim(ClaimTypes.Role, "Customer"));

            RenderedComponent<ProductDetailsWrapperTestComponent> component = _testHost.AddComponent<ProductDetailsWrapperTestComponent>(parameterView);

            component.Find("#ReviewFormRatingInput").Change("3");
            component.Find("#ReviewFormTextInput").Change("This is a test review.");
            component.Find("#ReviewForm").Submit();

            Assert.AreEqual("Bill", component.Find("#ReviewCustomerName").InnerText);
            Assert.AreEqual("3", component.Find("#ReviewRating").InnerText);
            Assert.AreEqual("This is a test review.", component.Find("#ReviewText").InnerText);
        }

        [Test]
        public void ProductDetailsPage_AuthenticatedAsCustomerOnPageWhereTheyHaveReview_CantSeeReviewForm()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>() { ["ProductID"] = "1" };

            ParameterView parameterView = AuthenticationStateService.PageParametersCreator(parameters, new Claim(ClaimTypes.Name, "Bob"), new Claim(ClaimTypes.Role, "Customer"));

            RenderedComponent<ProductDetailsWrapperTestComponent> component = _testHost.AddComponent<ProductDetailsWrapperTestComponent>(parameterView);

            Assert.AreEqual("Bill", component.Find("#ReviewCustomerName").InnerText);

            HtmlNode reviewForm = component.Find("#ReviewForm");

            Assert.IsNull(reviewForm);
        }

        [Test]
        public void ProductDetailsPage_AuthenticatedAsCustomerOnPageOfProductNotBought_CantSeeReviewForm()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>() { ["ProductID"] = "3" };

            ParameterView parameterView = AuthenticationStateService.PageParametersCreator(parameters, new Claim(ClaimTypes.Name, "Bob"), new Claim(ClaimTypes.Role, "Customer"));

            RenderedComponent<ProductDetailsWrapperTestComponent> component = _testHost.AddComponent<ProductDetailsWrapperTestComponent>(parameterView);

            HtmlNode reviewTableCell = component.Find("#ReviewCustomerName");

            Assert.IsNull(reviewTableCell);

            HtmlNode reviewForm = component.Find("#ReviewForm");

            Assert.IsNull(reviewForm);
        }

        [Test]
        public void ProductDetailsPage_AuthenticatedAsCustomerTriesToRemoveOwnReview_CanOnlyRemoveOwnReview()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>() { ["ProductID"] = "1" };

            ParameterView parameterView = AuthenticationStateService.PageParametersCreator(parameters, new Claim(ClaimTypes.Name, "Bob"), new Claim(ClaimTypes.Role, "Customer"));

            RenderedComponent<ProductDetailsWrapperTestComponent> component = _testHost.AddComponent<ProductDetailsWrapperTestComponent>(parameterView);

            IEnumerable<HtmlNode> tableRows = component.FindAll("tr").Where(tableRow => tableRow.Id.Contains("ReviewRow"));

            Assert.AreEqual(2, tableRows.Count());

            foreach (HtmlNode tableRow in tableRows)
            {
                if (tableRow.InnerText.Contains("Bob"))
                {
                    Assert.IsTrue(tableRow.InnerText.Contains("Delete"));
                }
                else if (tableRow.InnerText.Contains("Bill"))
                {
                    Assert.IsFalse(tableRow.InnerText.Contains("Delete"));
                }
            }

            component.Find("#ReviewDeleteButton").Click();

            tableRows = component.FindAll("tr").Where(tableRow => tableRow.Id.Contains("ReviewRow"));

            Assert.AreEqual(1, tableRows.Count());
        }

        [Test]
        public async Task ProductDetailsPage_AuthenticatedAsStaffTriesToRemoveAllReviews_CanRemoveAllReviews()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>() { ["ProductID"] = "1" };

            ParameterView parameterView = AuthenticationStateService.PageParametersCreator(parameters, new Claim(ClaimTypes.Name, "Lewis"), new Claim(ClaimTypes.Role, "Staff"));

            RenderedComponent<ProductDetailsWrapperTestComponent> component = _testHost.AddComponent<ProductDetailsWrapperTestComponent>(parameterView);

            IEnumerable<HtmlNode> tableRows = component.FindAll("tr").Where(tableRow => tableRow.Id.Contains("ReviewRow"));

            Assert.AreEqual(2, tableRows.Count());

            for (int x = 0; x < 2; x++)
            {
                ICollection<HtmlNode> deleteButtons = component.FindAll("#ReviewDeleteButton");

                deleteButtons.First().Click();
            }

            tableRows = component.FindAll("tr").Where(tableRow => tableRow.Id.Contains("ReviewRow"));

            Assert.AreEqual(0, tableRows.Count());
        }

        public class ProductDetailsWrapperTestComponent : ComponentBase
        {
            [Parameter] 
            public Task<AuthenticationState> AuthenticationState { get; set; }

            [Parameter]
            public string ProductID { get; set; }

            protected override void BuildRenderTree(RenderTreeBuilder builder)
            {
                builder.OpenComponent<CascadingValue<Task<AuthenticationState>>>(0);
                builder.AddAttribute(1, nameof(CascadingValue<Task<AuthenticationState>>.Value), AuthenticationState);
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(builder =>
                {
                    builder.OpenComponent<ProductDetails>(0);
                    builder.AddAttribute(1, nameof(ProductID), ProductID);
                    builder.CloseComponent();
                }));
                builder.CloseComponent();
            }
        }
    }
}
