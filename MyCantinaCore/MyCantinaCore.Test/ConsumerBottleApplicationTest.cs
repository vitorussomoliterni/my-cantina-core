using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using MyCantinaCore.Services;
using MyCantinaCore.DataAccess.Models;
using Microsoft.Extensions.DependencyInjection;
using MyCantinaCore.Commands.ConsumerBottle;

namespace MyCantinaCore.Test
{
    public class ConsumerBottleApplicationTest
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

        private static ConsumerBottle CreateNewConsumerBottle()
        {
            var consumerBottle = new ConsumerBottle()
            {
                ConsumerId = 1,
                BottleId = 1,
                DateAcquired = DateTime.Now,
                DateOpened = (DateTime)DateTime.Now,
                Qty = 1,
                Owned = true,
                PricePaid = 18
            };

            return consumerBottle;
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
    }
}
