using Microsoft.EntityFrameworkCore;

namespace DevOps.Products.PriceHistories.DAL
{
    public class PriceHistoryContext : DbContext
    {
        public DbSet<PriceHistory> PriceHistories { get; set; }

        public PriceHistoryContext()
        {
        }

        public PriceHistoryContext(DbContextOptions<PriceHistoryContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PriceHistory>().HasKey(x => x.ID);
            modelBuilder.Entity<PriceHistory>(x =>
            {
                x.Property(y => y.ID).ValueGeneratedOnAdd();
                x.Property(y => y.Price).IsRequired();
                x.Property(y => y.ProductID).IsRequired();
            });
        }
    }
}
