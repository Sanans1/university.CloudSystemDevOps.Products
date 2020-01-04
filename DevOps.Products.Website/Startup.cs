using AutoMapper;
using DevOps.Products.Website.Services.Fakes.Facades;
using DevOps.Products.Website.Services.Implementations.Facades;
using DevOps.Products.Website.Services.Interfaces.Facades;
using Flurl.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DevOps.Products.Website
{
    public class Startup
    {
        private const bool SHOULD_MOCK_PRODUCT_FACADE = false;
        private const bool SHOULD_MOCK_CATEGORY_FACADE = false;
        private const bool SHOULD_MOCK_BRAND_FACADE = false;
        private const bool SHOULD_MOCK_REVIEW_FACADE = false;
        private const bool SHOULD_MOCK_CUSTOMER_FACADE = true;
        private const bool SHOULD_MOCK_ORDER_FACADE = true;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(
                    CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie();

            services.AddRazorPages();
            services.AddServerSideBlazor();

            //NuGet Package Services
            services.AddAutoMapper(typeof(Startup));
            FlurlHttp.Configure(settings => settings.HttpClientFactory = new PollyHttpClientFactory());

            //services.AddHttpContextAccessor();
            //services.AddScoped<HttpContextAccessor>();

            //Facades
            if (SHOULD_MOCK_PRODUCT_FACADE && Environment.IsDevelopment()) 
                services.AddSingleton<IProductFacadeService, FakeProductFacadeService>(); 
            else 
                services.AddScoped<IProductFacadeService, ProductFacadeService>();

            if (SHOULD_MOCK_CATEGORY_FACADE && Environment.IsDevelopment()) 
                services.AddSingleton<ICategoryFacadeService, FakeCategoryFacadeService>();
            else
                services.AddScoped<ICategoryFacadeService, CategoryFacadeService>();

            if (SHOULD_MOCK_BRAND_FACADE && Environment.IsDevelopment()) 
                services.AddSingleton<IBrandFacadeService, FakeBrandFacadeService>();
            else
                services.AddScoped<IBrandFacadeService, BrandFacadeService>();

            if (SHOULD_MOCK_REVIEW_FACADE && Environment.IsDevelopment()) 
                services.AddSingleton<IReviewFacadeService, FakeReviewFacadeService>();
            else
                services.AddScoped<IReviewFacadeService, ReviewFacadeService>();

            if (SHOULD_MOCK_CUSTOMER_FACADE)
                services.AddSingleton<ICustomerFacadeService, FakeCustomerFacadeService>();
            else
                services.AddScoped<ICustomerFacadeService, CustomerFacadeService>();

            if (SHOULD_MOCK_ORDER_FACADE)
                services.AddSingleton<IOrderFacadeService, FakeOrderFacadeService>();
            else
                services.AddScoped<IOrderFacadeService, OrderFacadeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseForwardedHeaders();

            if (Environment.IsDevelopment())
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

            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
