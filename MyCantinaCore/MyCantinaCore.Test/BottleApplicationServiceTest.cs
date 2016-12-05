using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using MyCantinaCore.Services;
using MyCantinaCore.DataAccess.Models;
using Microsoft.Extensions.DependencyInjection;
using MyCantinaCore.Commands.Bottle;

namespace MyCantinaCore.Test
{
    public class BottleApplicationServiceTest
    {
        private static DbContextOptions<MyCantinaCoreDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<MyCantinaCoreDbContext>();
            builder.UseInMemoryDatabase()
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        private static Bottle CreateNewBotle()
        {
            var bottle = new Bottle()
            {
                Name = "Terra rossa",
                Year = "2014",
                Producer = "Producer of Fine Wine",
                Description = "A robust red, for pasta lovers",
                WineType = "Red",
                Region = "Basilicata",
                Country = "Italy",
                AverageRating = 4.8
            };

            return bottle;
        }

        [Fact]
        private async static Task AddBottleTest_ShouldAddNewBottleIntoDb()
        {
            var options = CreateNewContextOptions();

            using (var context = new MyCantinaCoreDbContext(options))
            {
                // Fixture
                var service = new BottleApplicationService(context);

                var command = new AddBottleCommand()
                {
                    Name = "Terra rossa",
                    Year = "2014",
                    Producer = "Producer of Fine Wine",
                    Description = "A robust red, for pasta lovers",
                    WineType = "Red",
                    Region = "Basilicata",
                    Country = "Italy"
                };

                command.GrapeVarietyIds = new List<int>() { 1, 2 };

                await context.GrapeVarieties.AddAsync(new GrapeVariety()
                {
                    Name = "Aglianico",
                    Colour = "Red"
                });

                await context.GrapeVarieties.AddAsync(new GrapeVariety()
                {
                    Name = "Merlot",
                    Colour = "Red"
                });

                await context.SaveChangesAsync();

                var expectedBottle = new Bottle()
                {
                    Id = 1,
                    Name = "Terra Rossa",
                    Year = "2014",
                    Producer = "Producer of Fine Wine",
                    Description = "A robust red, for pasta lovers",
                    WineType = "Red",
                    Region = "Vulture",
                    Country = "Italy",
                    AverageRating = 0
                };

                // SUT
                await service.AddBottle(command);

                var actualBottle = await context.Bottles.FirstOrDefaultAsync();

                // Verify Outcome
                Assert.NotEmpty(context.Bottles);
            }
        }
    }
}
