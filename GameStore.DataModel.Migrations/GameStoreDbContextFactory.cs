using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GameStore.DataModel.Migrations
{
    public class GameStoreDbContextFactory
        : IDesignTimeDbContextFactory<GameStoreDbContext>
    {
        public GameStoreDbContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            var configuration = configurationBuilder.Build();
            var connectionString = configuration
                .GetConnectionString("GameStoreDB");

            var optionsBuilder = new DbContextOptionsBuilder<GameStoreDbContext>()
                .UseSqlServer(connectionString,
                    b
                        => b.MigrationsAssembly("GameStore.DataModel.Migrations"));

            return new GameStoreDbContext(optionsBuilder.Options);
        }
    }
}