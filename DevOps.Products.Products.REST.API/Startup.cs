using AutoMapper;
using DevOps.Products.Common.Repository;
using DevOps.Products.Products.DAL;
using DevOps.Products.Products.REST.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DevOps.Products.Products.REST.API
{
    public class Startup
    {
        private const bool SHOULD_USE_IN_MEMORY_DB = false;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));

            if (SHOULD_USE_IN_MEMORY_DB)
            {
                services.AddDbContext<ProductContext>(options => options.UseLazyLoadingProxies()
                                                                                   .UseInMemoryDatabase("Products"));
            }
            else
            {
                string databaseConnectionString = Configuration.GetConnectionString("ProductDatabase");
                services.AddDbContext<ProductContext>(options => options.UseLazyLoadingProxies()
                    .UseSqlServer(databaseConnectionString));
            }

            services.AddScoped<IGenericRepository<Product, ProductDTO>, GenericRepository<ProductContext, Product, ProductDTO>>();
            services.AddScoped<IGenericRepository<Category, CategoryDTO>, GenericRepository<ProductContext, Category, CategoryDTO>>();
            services.AddScoped<IGenericRepository<Brand, BrandDTO>, GenericRepository<ProductContext, Brand, BrandDTO>>();
            services.AddScoped<IGenericRepository<PriceHistory, PriceHistoryDTO>, GenericRepository<ProductContext, PriceHistory, PriceHistoryDTO>>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Products REST API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Products REST API");
                options.RoutePrefix = "swagger";
            });
        }
    }
}
