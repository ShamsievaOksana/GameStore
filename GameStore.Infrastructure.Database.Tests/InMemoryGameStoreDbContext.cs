using System;
using GameStore.DataModel;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Database.Tests
{
    public class InMemoryGameStoreDbContext
        : GameStoreDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseInMemoryDatabase($"TEST_{DateTime.UtcNow.Ticks}");
        }
    }
}