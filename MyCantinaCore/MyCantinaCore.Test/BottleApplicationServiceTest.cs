using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using MyCantinaCore.Services;
using MyCantinaCore.DataAccess.Models;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
