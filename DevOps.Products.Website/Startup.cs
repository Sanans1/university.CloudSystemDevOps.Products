using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevOps.Products.Website.Services.Fakes.Facades;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DevOps.Products.Website.Services.Implementations;
using DevOps.Products.Website.Services.Implementations.Facades;
using DevOps.Products.Website.Services.Implementations.Pages;
using DevOps.Products.Website.Services.Interfaces;
using DevOps.Products.Website.Services.Interfaces.Facades;
using DevOps.Products.Website.Services.Interfaces.Pages;
using DevOps.Products.Website.Services.Fakes;
using Flurl.Http;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace DevOps.Products.Website
{
    public class Startup
    {
        private const bool SHOULD_MOCK_PRODUCT_FACADE = false;
        private const bool SHOULD_MOCK_CATEGORY_FACADE = false;
        private const bool SHOULD_MOCK_BRAND_FACADE = false;
        private const bool SHOULD_MOCK_REVIEW_FACADE = false;
        private const bool SHOULD_MOCK_CUSTOMER_FACADE = true;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            //NuGet Package Services
            services.AddAutoMapper(typeof(Startup));
            FlurlHttp.Configure(settings => settings.HttpClientFactory = new PollyHttpClientFactory());

            //Pages
            services.AddScoped<IProductListService, ProductListService>();
            services.AddScoped<IProductDetailsService, ProductDetailsService>();
            
            //Facades
            if (SHOULD_MOCK_PRODUCT_FACADE) 
                services.AddSingleton<IProductFacadeService, FakeProductFacadeService>(); 
            else 
                services.AddScoped<IProductFacadeService, ProductFacadeService>();

            if (SHOULD_MOCK_CATEGORY_FACADE) 
                services.AddSingleton<ICategoryFacadeService, FakeCategoryFacadeService>();
            else
                services.AddScoped<ICategoryFacadeService, CategoryFacadeService>();

            if (SHOULD_MOCK_BRAND_FACADE) 
                services.AddSingleton<IBrandFacadeService, FakeBrandFacadeService>();
            else
                services.AddScoped<IBrandFacadeService, BrandFacadeService>();

            if (SHOULD_MOCK_REVIEW_FACADE) 
                services.AddSingleton<IReviewFacadeService, FakeReviewFacadeService>();
            else
                services.AddScoped<IReviewFacadeService, ReviewFacadeService>();

            if (SHOULD_MOCK_CUSTOMER_FACADE) 
                services.AddSingleton<ICustomerFacadeService, FakeCustomerFacadeService>();
            else
                services.AddScoped<ICustomerFacadeService, CustomerFacadeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
