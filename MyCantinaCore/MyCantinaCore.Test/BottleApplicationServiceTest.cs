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
                Name = "Terra Rossa",
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
                    Name = "Terra Rossa",
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

                var expectedBottle = CreateNewBotle();
                expectedBottle.Id = 1;
                expectedBottle.AverageRating = 0;

                // SUT
                await service.AddBottle(command);

                var actualBottle = await context.Bottles.Include(b => b.BottleGrapeVarieties).FirstOrDefaultAsync();

                // Verify Outcome
                Assert.NotEmpty(context.Bottles);
                Assert.Equal(1, context.Bottles.Count());
                Assert.NotNull(actualBottle);
                Assert.Equal(expectedBottle.Id, actualBottle.Id);
                Assert.Equal(expectedBottle.Name, actualBottle.Name);
                Assert.Equal(expectedBottle.Year, actualBottle.Year);
                Assert.Equal(expectedBottle.Producer, actualBottle.Producer);
                Assert.Equal(expectedBottle.Description, actualBottle.Description);
                Assert.Equal(expectedBottle.WineType, actualBottle.WineType);
                Assert.Equal(expectedBottle.Region, actualBottle.Region);
                Assert.Equal(expectedBottle.Country, actualBottle.Country);
                Assert.Equal(expectedBottle.AverageRating, actualBottle.AverageRating);
                Assert.NotEmpty(actualBottle.BottleGrapeVarieties);
                Assert.Equal(actualBottle.BottleGrapeVarieties.FirstOrDefault().GrapeVariety, await context.GrapeVarieties.FirstOrDefaultAsync(gv => gv.Id == 1));
                Assert.Equal(actualBottle.BottleGrapeVarieties.LastOrDefault().GrapeVariety, await context.GrapeVarieties.FirstOrDefaultAsync(gv => gv.Id == 2));
            }
        }

        [Fact]
        public async static Task UpdateBottleTest_ShouldUpdateBottleInDb()
        {
            var options = CreateNewContextOptions();

            using (var context = new MyCantinaCoreDbContext(options))
            {
                // Fixture
                var service = new BottleApplicationService(context);

                var originalBottle = CreateNewBotle();

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

                originalBottle.BottleGrapeVarieties.Add(new BottleGrapeVariety()
                {
                    BottleId = 1,
                    GrapeVarietyId = 1
                });

                await context.Bottles.AddAsync(originalBottle);

                await context.SaveChangesAsync();

                var command = new UpdateBottleCommand()
                {
                    Id = 1,
                    Name = "Fall Song",
                    Year = "2015",
                    Producer = "The farmer",
                    Description = "A light rosé, for fall evenings",
                    WineType = "Rosé",
                    Region = "Orange County",
                    Country = "Australia"
                };

                command.GrapeVarietyIds.Add(2);

                // S.U.T.

                await service.UpdateBottle(command);

                var actualBottle = await context.Bottles.FirstOrDefaultAsync();

                // Verify Outcome
                Assert.NotEmpty(context.Bottles);
                Assert.Equal(1, context.Bottles.Count());
                Assert.NotNull(actualBottle);
                Assert.Equal(command.Id, actualBottle.Id);
                Assert.Equal(command.Name, actualBottle.Name);
                Assert.Equal(command.Year, actualBottle.Year);
                Assert.Equal(command.Producer, actualBottle.Producer);
                Assert.Equal(command.Description, actualBottle.Description);
                Assert.Equal(command.WineType, actualBottle.WineType);
                Assert.Equal(command.Region, actualBottle.Region);
                Assert.Equal(command.Country, actualBottle.Country);
                Assert.NotEmpty(actualBottle.BottleGrapeVarieties);
                Assert.Equal(1, actualBottle.BottleGrapeVarieties.Count);
                Assert.Equal(actualBottle.BottleGrapeVarieties.FirstOrDefault().GrapeVariety, await context.GrapeVarieties.FirstOrDefaultAsync(gv => gv.Id == 2));
            }
        }

        [Fact]
        public async static Task DeleteBottleTest_ShouldDeleteBottleFromDb()
        {
            var options = CreateNewContextOptions();

            using (var context = new MyCantinaCoreDbContext(options))
            {
                // Fixture
                var service = new BottleApplicationService(context);

                var originalBottle = CreateNewBotle();

                await context.Bottles.AddAsync(originalBottle);
                await context.SaveChangesAsync();

                // S.U.T.
                await service.DeleteBottle(1);

                // Verify Outcome
                Assert.Empty(context.Bottles);
            }
        }
    }
}
