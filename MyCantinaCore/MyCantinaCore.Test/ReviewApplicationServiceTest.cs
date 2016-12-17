using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using MyCantinaCore.Services;
using MyCantinaCore.DataAccess.Models;
using Microsoft.Extensions.DependencyInjection;
using MyCantinaCore.Commands.Review;

namespace MyCantinaCore.Test
{
    public class ReviewApplicationServiceTest
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

        private static Review CreateNewReview()
        {
            var review = new Review()
            {
                Body = "Very nice wine. 10 out of 10, would hangover again.",
                BottleId = 1,
                ConsumerId = 1,
                Rating = 5,
                DatePosted = DateTime.UtcNow
            };

            return review;
        }

        private static Consumer CreateNewConsumer()
        {
            var consumer = new Consumer()
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@email.com",
                DateOfBirth = new DateTime(1982, 9, 13)
            };

            return consumer;
        }

        private static Bottle CreateNewBottle()
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
        public async static Task AddReviewTest_ShouldAddReviewIntoDb()
        {
            var options = CreateNewContextOptions();

            using (var context = new MyCantinaCoreDbContext(options))
            {
                // Fixture
                var service = new ReviewApplicationService(context);

                await context.Bottles.AddAsync(CreateNewBottle());
                await context.Consumers.AddAsync(CreateNewConsumer());
                await context.SaveChangesAsync();

                var command = new ReviewCommand()
                {
                    Body = "Very nice wine. 10 out of 10, would hangover again.",
                    BottleId = 1,
                    ConsumerId = 1,
                    Rating = 5,
                };

                // S.U.T.
                await service.AddReview(command);

                var actualReview = await context.Reviews.FirstOrDefaultAsync(r => r.Id == 1);
                var actualBottle = await context.Bottles.FirstOrDefaultAsync(b => b.Id == 1);

                // Verify Outcome
                Assert.NotEmpty(context.Reviews);
                Assert.Equal(1, context.Reviews.Count());
                Assert.Equal(1, actualReview.Id);
                Assert.Equal(command.Body, actualReview.Body);
                Assert.Equal(command.BottleId, actualReview.BottleId);
                Assert.Equal(command.ConsumerId, actualReview.ConsumerId);
                Assert.Equal(command.Rating, actualReview.Rating);
                Assert.Equal(command.Rating, actualBottle.AverageRating);
            }
        }

        [Fact]
        public async static Task UpdateReviewText_ShouldUpdateReviewInDb()
        {
            var options = CreateNewContextOptions();

            using (var context = new MyCantinaCoreDbContext(options))
            {
                // Fixture
                var service = new ReviewApplicationService(context);

                await context.Bottles.AddAsync(CreateNewBottle());
                await context.Consumers.AddAsync(CreateNewConsumer());
                await context.Reviews.AddAsync(CreateNewReview());
                await context.SaveChangesAsync();

                var command = new ReviewCommand()
                {
                    Id = 1,
                    Body = "A bit meh.",
                    BottleId = 1,
                    ConsumerId = 1,
                    Rating = 3,
                };

                // S.U.T.
                await service.UpdateReview(command);

                var actualReview = await context.Reviews.FirstOrDefaultAsync(r => r.Id == command.Id);
                var actualBottle = await context.Bottles.FirstOrDefaultAsync(b => b.Id == 1);

                // Verify Outcome
                Assert.NotEmpty(context.Reviews);
                Assert.Equal(1, context.Reviews.Count());
                Assert.Equal(command.Id, actualReview.Id);
                Assert.Equal(command.Body, actualReview.Body);
                Assert.Equal(command.BottleId, actualReview.BottleId);
                Assert.Equal(command.ConsumerId, actualReview.ConsumerId);
                Assert.Equal(command.Rating, actualReview.Rating);
                Assert.Equal(command.Rating, actualBottle.AverageRating);
            }
        }

        [Fact]
        public async static Task DeleteReviewTest_ShouldRemoveReviewFromDb()
        {
            var options = CreateNewContextOptions();

            using (var context = new MyCantinaCoreDbContext(options))
            {
                // Fixture
                var service = new ReviewApplicationService(context);

                await context.Bottles.AddAsync(CreateNewBottle());
                await context.Consumers.AddAsync(CreateNewConsumer());
                await context.Reviews.AddAsync(CreateNewReview());
                await context.SaveChangesAsync();

                // S.U.T.
                await service.DeleteReview(1);
                var actualBottle = await context.Bottles.FirstOrDefaultAsync(b => b.Id == 1);

                // Verify Outcome
                Assert.Empty(context.Reviews);
            }
        }
    }
}
