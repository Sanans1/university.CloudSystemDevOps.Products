﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DevOps.Product.Common.Repository.Tests
{
    public class FakeContext : DbContext
    {
        public DbSet<FakeEntity> TestEntities { get; set; }

        public FakeContext() 
        { }

        public FakeContext(DbContextOptions<FakeContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FakeEntity>().HasKey(x => x.ID);
            modelBuilder.Entity<FakeEntity>(x =>
            {
                x.Property(y => y.ID).ValueGeneratedOnAdd();
                x.Property(y => y.Text).IsRequired();
            });
        }
    }
}
