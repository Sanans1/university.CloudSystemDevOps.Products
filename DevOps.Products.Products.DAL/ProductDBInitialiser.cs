using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace DevOps.Products.Products.DAL
{
    public static class ProductDBInitialiser
    {
        public static async Task SeedTestData(ProductContext context,
                                      IServiceProvider services)
        {
            if (context.Products.Any())
            {
                //db seems to be seeded
                return;
            }

            List<Category> categories = new List<Category>
            {
                new Category { Name = "Covers", IsActive = true },
                new Category { Name = "Case", IsActive = false },
                new Category { Name = "Accessories", IsActive = true },
                new Category { Name = "Screen Protectors", IsActive = true }
            };
            categories.ForEach(c => context.Categories.Add(c));

            List<Brand> brands = new List<Brand>
            {
                new Brand { Name = "Soggy Sponge", IsActive = true },
                new Brand { Name = "Damp Squib", IsActive = false },
                new Brand { Name = "iStuff-R-Us", IsActive = true }
            };
            brands.ForEach(b => context.Brands.Add(b));

            await context.SaveChangesAsync();

            var products = new List<Product>
            {
                new Product { IsActive = true, Brand = brands[0], Category = categories[0], Description = "Poor quality fake faux leather cover loose enough to fit any mobile device.", Name = "Wrap It and Hope Cover", Price = 5.99m, Quantity = 1 },
                new Product { IsActive = true, Brand = brands[1], Category = categories[0], Description = "Purchase you favourite chocolate and use the provided heating element to melt it into the perfect cover for your mobile device.", Name = "Chocolate Cover", Price = 10.97m, Quantity = 0 },
                new Product { IsActive = true, Brand = brands[2], Category = categories[0], Description = "Lamely adapted used and dirty teatowel.  Guaranteed fewer than two holes.", Name = "Cloth Cover", Price = 3.01m, Quantity = 6 },
                new Product { IsActive = true, Brand = brands[0], Category = categories[1], Description = "Especially toughen and harden sponge entirely encases your device to prevent any interaction.", Name = "Harden Sponge Case", Price = 9.99m, Quantity = 2 },
                new Product { IsActive = true, Brand = brands[0], Category = categories[1], Description = "Place your device within the water-tight container, fill with water and enjoy the cushioned protection from bumps and bangs.", Name = "Water Bath Case", Price = 20.0m, Quantity = 3 },
                new Product { IsActive = false, Brand = brands[0], Category = categories[2], Description = "Keep you smartphone handsfree with this large assembly that attaches to your rear window wiper (Hatchbacks only).", Name = "Smartphone Car Holder", Price = 110.01m, Quantity = 8 },
                new Product { IsActive = true, Brand = brands[0], Category = categories[2], Description = "Keep your device on your arm with this general purpose sticky tape.", Name = "Sticky Tape Sport Armband", Price = 2.99m, Quantity = 23 },
                new Product { IsActive = true, Brand = brands[1], Category = categories[2], Description = "Stengthen HB pencils guaranteed to leave a mark.", Name = "Real Pencil Stylus", Price = 0.99m, Quantity = 5 },
                new Product { IsActive = true, Brand = brands[0], Category = categories[3], Description = "Coat your mobile device screen in a scratch resistant, opaque film.", Name = "Spray Paint Screen Protector", Price = 4.99m, Quantity = 1 },
                new Product { IsActive = true, Brand = brands[2], Category = categories[3], Description = "For his or her sensory pleasure. Fits few known smartphones.", Name = "Rippled Screen Protector", Price = 7.99m, Quantity = 5 },
                new Product { IsActive = true, Brand = brands[2], Category = categories[3], Description = "For an odour than lingers on your device.", Name = "Fish Scented Screen Protector", Price = 2.88m, Quantity = 0 },
                new Product { IsActive = true, Brand = brands[1], Category = categories[3], Description = "Guaranteed not to conduct electical charge from your fingers.", Name = "Non-conductive Screen Protector", Price = 10.0m, Quantity = 10 },
            };
            products.ForEach(p => context.Products.Add(p));

            await context.SaveChangesAsync();
        }
    }
}
