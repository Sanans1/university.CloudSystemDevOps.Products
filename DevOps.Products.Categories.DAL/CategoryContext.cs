using Microsoft.EntityFrameworkCore;

namespace DevOps.Products.Categories.DAL
{
    public class CategoryContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public CategoryContext()
        {
        }

        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasKey(x => x.ID);
            modelBuilder.Entity<Category>(x =>
            {
                x.Property(y => y.ID).ValueGeneratedOnAdd();
                x.Property(y => y.Name).IsRequired();
            });
        }
    }
}
