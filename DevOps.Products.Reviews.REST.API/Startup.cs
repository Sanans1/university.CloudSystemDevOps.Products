using AutoMapper;
using DevOps.Products.Common.Repository;
using DevOps.Products.Reviews.DAL;
using DevOps.Products.Reviews.REST.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DevOps.Products.Reviews.REST.API
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
                services.AddDbContext<ReviewContext>(options => options.UseInMemoryDatabase("Reviews"));
            }
            else
            {
                string databaseConnectionString = Configuration.GetConnectionString("ReviewDatabase");
                services.AddDbContext<ReviewContext>(options => options.UseSqlServer(databaseConnectionString));
            }

            services.AddScoped<IGenericRepository<Review, ReviewDTO>, GenericRepository<ReviewContext, Review, ReviewDTO>>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Reviews REST API", Version = "v1" });
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
                options.RouteTemplate = "api/review-management/swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("swagger/v1/swagger.json", "Reviews REST API");
                options.RoutePrefix = "swagger";
            });
        }
    }
}
