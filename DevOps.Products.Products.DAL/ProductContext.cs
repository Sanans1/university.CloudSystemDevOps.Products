using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DevOps.Products.Products.DAL
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<PriceHistory> PriceHistories { get; set; }

        public ProductContext()
        {
        }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(productEntities =>
            {
                productEntities.HasKey(product => product.ID);
                productEntities.Property(product => product.ID).ValueGeneratedOnAdd();
                productEntities.Property(product => product.Name).IsRequired();

                productEntities.HasOne(product => product.Category)
                               .WithMany()
                               .HasForeignKey(product => product.CategoryID);

                productEntities.HasOne(product => product.Brand)
                               .WithMany()
                               .HasForeignKey(product => product.BrandID);

                productEntities.HasMany(product => product.PriceHistories)
                               .WithOne()
                               .HasForeignKey(x => x.ProductID);
            });
        }
    }
}
