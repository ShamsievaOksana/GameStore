using Microsoft.EntityFrameworkCore;

namespace GameStore.DataModel
{
    public class GameStoreDbContext
        : DbContext
    {
        public virtual DbSet<ProductEntity> Products { get; set; }

        public GameStoreDbContext()
        {
            
        }

        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
        }
    }
}