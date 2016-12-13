using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using MyCantinaCore.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyCantinaCore.Services;
using MyCantinaCore.Commands.Consumer;

namespace MyCantinaCore.Test
{
    public class ConsumerApplicationServiceTest
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

        private static Consumer CreateNewConsumer()
        {
            var consumer = new Consumer()
            {
                FirstName = "Jane",
                LastName = "Doe",
                DateOfBirth = new DateTime(1984, 9, 21),
                Email = "jane.doe@email.com"
            };

            return consumer;
        }

        [Fact]
        public static async Task AddConsumerTest_ShouldAddConsumerInDb()
        {
            var options = CreateNewContextOptions();

            using (var context = new MyCantinaCoreDbContext(options))
            {
                // Fixture
                var service = new ConsumerApplicationService(context);

                var command = new AddCounsumerCommand()
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1984, 9, 21),
                    Email = "jane.doe@email.com"
                };

                var expectedConsumer = CreateNewConsumer();

                // S.U.T.
                await service.AddConsumer(command);

                var actualConsumer = await context.Consumers.FirstOrDefaultAsync();

                // Verify Outcome
                Assert.NotEmpty(context.GrapeVarieties);
                Assert.Equal(1, context.Consumers.Count());
                Assert.Equal(1, actualConsumer.Id);
                Assert.Equal(expectedConsumer.FirstName, actualConsumer.FirstName);
                Assert.Equal(expectedConsumer.LastName, actualConsumer.LastName);
                Assert.Equal(expectedConsumer.DateOfBirth, actualConsumer.DateOfBirth);
                Assert.Equal(expectedConsumer.Email, actualConsumer.Email);
            }
        }
    }
}
