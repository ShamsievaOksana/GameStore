using System;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DataModel
{
    public class InMemoryGameStoreDbContext
        : GameStoreDbContext
    {
        private readonly string _databaseName;

        public InMemoryGameStoreDbContext(string databaseName)
        {
            _databaseName = databaseName;
        }

        public InMemoryGameStoreDbContext()
            : this($"TEST_{DateTime.UtcNow.Ticks}")
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseInMemoryDatabase(_databaseName);
        }
    }
}