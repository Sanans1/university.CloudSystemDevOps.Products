using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DevOps.Products.Brands.DAL
{
    public class BrandContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }

        public BrandContext()
        {
        }

        public BrandContext(DbContextOptions<BrandContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brand>().HasKey(x => x.ID);
            modelBuilder.Entity<Brand>(x =>
            {
                x.Property(y => y.ID).ValueGeneratedOnAdd();
                x.Property(y => y.Name).IsRequired();
            });
        }
    }
}
