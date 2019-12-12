using System;
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
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));

            if (Environment.IsDevelopment())
            {
                services.AddDbContext<ProductContext>(options => options.UseLazyLoadingProxies()
                                                                                    .UseInMemoryDatabase("Products"));
            }
            else
            {
                services.AddDbContext<ProductContext>(options => options.UseLazyLoadingProxies()
                                                                                    .UseSqlServer(Configuration.GetConnectionString("ProductDatabase")));
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
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

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
                options.RoutePrefix = "swagger";
                options.SwaggerEndpoint("v1/swagger.json", "Products REST API");
            });
        }
    }
}
