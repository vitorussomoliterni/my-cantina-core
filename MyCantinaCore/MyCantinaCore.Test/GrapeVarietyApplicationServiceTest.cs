using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using MyCantinaCore.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyCantinaCore.Services;

namespace MyCantinaCore.Test
{
    public class GrapeVarietyApplicationServiceTest
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

        private static GrapeVariety CreateNewGrapeVariety()
        {
            var grapeVariety = new GrapeVariety()
            {
                Name = "Aglianico",
                Colour = "Red"
            };

            return grapeVariety;
        }

        [Fact]
        public static async Task AddGrapeVarietyTest_ShouldAddGrapeVarietyIntoDb()
        {
            var options = CreateNewContextOptions();

            using (var context = new MyCantinaCoreDbContext(options))
            {
                // Fixture
                var service = new GrapeVarietyApplicationService(context);

                var expectedGrapeVariety = CreateNewGrapeVariety();

                // S.U.T.
                await service.AddGrapeVariety(expectedGrapeVariety.Name, expectedGrapeVariety.Colour);

                var actualGrapeVariety = await context.GrapeVarieties.FirstOrDefaultAsync();

                // Verify Outcome
                Assert.NotEmpty(context.GrapeVarieties);
                Assert.Equal(1, context.GrapeVarieties.Count());
                Assert.Equal(1, actualGrapeVariety.Id);
                Assert.Equal(expectedGrapeVariety.Name, actualGrapeVariety.Name);
                Assert.Equal(expectedGrapeVariety.Colour, actualGrapeVariety.Colour);
            }
        }

        [Fact]
        public static async Task UpdateGrapeVarietyTest_ShouldUpdateGrapeVarietyIntoDb()
        {
            var options = CreateNewContextOptions();

            using (var context = new MyCantinaCoreDbContext(options))
            {
                // Fixture
                var service = new GrapeVarietyApplicationService(context);

                var grapeVariety = CreateNewGrapeVariety();
                await context.GrapeVarieties.AddAsync(grapeVariety);
                await context.SaveChangesAsync();

                var expectedGrapeVariety = new GrapeVariety()
                {
                    Name = "Greco di Tufo",
                    Colour = "White"
                };

                // S.U.T.
                await service.UpdateGrapeVariety(1, expectedGrapeVariety.Name, expectedGrapeVariety.Colour);

                var actualGrapeVariety = await context.GrapeVarieties.FirstOrDefaultAsync();

                // Verify Outcome
                Assert.NotEmpty(context.GrapeVarieties);
                Assert.Equal(1, context.GrapeVarieties.Count());
                Assert.Equal(1, actualGrapeVariety.Id);
                Assert.Equal(expectedGrapeVariety.Name, actualGrapeVariety.Name);
                Assert.Equal(expectedGrapeVariety.Colour, actualGrapeVariety.Colour);
            }
        }

        [Fact]
        public static async Task DeleteGrapeVariety_ShouldRemoveGrapeVarietyFromDb()
        {
            var options = CreateNewContextOptions();

            using (var context = new MyCantinaCoreDbContext(options))
            {
                // Fixture
                var service = new GrapeVarietyApplicationService(context);

                var grapeVariety = CreateNewGrapeVariety();
                await context.GrapeVarieties.AddAsync(grapeVariety);
                await context.SaveChangesAsync();

                // S.U.T.
                await service.DeleteGrapeVariety(1);

                // Verify Outcome
                Assert.Empty(context.GrapeVarieties);
            }
        }
    }
}
