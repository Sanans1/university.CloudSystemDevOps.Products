using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DevOps.Products.Reviews.DAL
{
    public class ReviewContext : DbContext
    {
        public DbSet<Review> Categories { get; set; }

        public ReviewContext()
        {
        }

        public ReviewContext(DbContextOptions<ReviewContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Review>().HasKey(x => x.ID);
            modelBuilder.Entity<Review>(x =>
            {
                x.Property(y => y.ID).ValueGeneratedOnAdd();
                x.Property(y => y.Rating).IsRequired();
                x.Property(y => y.Text).IsRequired();
            });
        }
    }
}
