using System;
using System.Linq;
using System.Threading.Tasks;
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
        
        public override int SaveChanges()
        {
            SetDefaultDateTimeValues();

            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            SetDefaultDateTimeValues();

            return await base.SaveChangesAsync();
        }
        
        private void SetDefaultDateTimeValues()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State is EntityState.Added or EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity) entityEntry.Entity).Modified = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity) entityEntry.Entity).Created = DateTime.UtcNow;
                }
            }
        }
    }
}