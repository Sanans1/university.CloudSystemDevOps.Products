using System;
using DevOps.Products.Products.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DevOps.Products.Products.REST.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                ProductContext context = services.GetRequiredService<ProductContext>();

                IWebHostEnvironment env = services.GetRequiredService<IWebHostEnvironment>();
                if (env.IsDevelopment())
                {
                    try
                    {
                        ProductDBInitialiser.SeedTestData(context, services).Wait();
                    }
                    catch (Exception)
                    {
                        ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogDebug("Seeding test data failed.");
                    }
                }
                else
                {
                    context.Database.Migrate();
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
