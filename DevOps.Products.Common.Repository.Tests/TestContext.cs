using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DevOps.Product.Common.Repository.Tests
{
    public class TestContext : DbContext
    {
        public DbSet<TestEntity> TestEntities { get; set; }

        public TestContext() 
        { }

        public TestContext(DbContextOptions<TestContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TestEntity>().HasKey(x => x.ID);
            modelBuilder.Entity<TestEntity>(x =>
            {
                x.Property(y => y.ID).ValueGeneratedOnAdd();
                x.Property(y => y.Text).IsRequired();
            });
        }
    }
}
