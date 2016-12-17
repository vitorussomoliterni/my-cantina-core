using System;
using System.Linq;

namespace MyCantinaCore.DataAccess.Models
{
    public static class MyCantinaCoreExtensions
    {
        public static void EnsureSeedData(this MyCantinaCoreDbContext context)
        {
            if (!context.GrapeVarieties.Any())
            {
                context.GrapeVarieties.AddRange(
                new GrapeVariety()
                {
                    Name = "Aglianico",
                    Colour = "Red"
                },
                new GrapeVariety()
                {
                    Name = "Pecorino",
                    Colour = "White"
                });

                context.SaveChanges();
            }

            if (!context.Bottles.Any())
            {
                context.Bottles.AddRange(
                    new Bottle()
                    {
                        Name = "Vino Contadino",
                        Year = "2015",
                        Producer = "Il Contadino",
                        Description = "A robust red",
                        WineType = "Red",
                        Region = "Campania",
                        Country = "Italy",
                        AverageRating = 0
                    },
                    new Bottle()
                    {
                        Name = "Vino Cittadino",
                        Year = "2015",
                        Producer = "Il Cittadino",
                        Description = "A fine white",
                        WineType = "White",
                        Region = "Lazio",
                        Country = "Italy",
                        AverageRating = 0
                    });

                context.SaveChanges();

                var firstBottle = context.Bottles.FirstOrDefault(b => b.Id == 1);
                firstBottle.BottleGrapeVarieties.Add(new BottleGrapeVariety()
                {
                    GrapeVarietyId = 1,
                    GrapeVarietyName = "Aglianico",
                    GrapeVarietyColour = "Red"
                });
                var secondBottle = context.Bottles.FirstOrDefault(b => b.Id == 2);
                secondBottle.BottleGrapeVarieties.Add(new BottleGrapeVariety()
                {
                    GrapeVarietyId = 2,
                    GrapeVarietyName = "Pecorino",
                    GrapeVarietyColour = "White"
                });

                context.SaveChanges();
            }

            if (!context.Consumers.Any())
            {
                context.Consumers.AddRange(
                    new Consumer()
                    {
                        FirstName = "Jane",
                        LastName = "Doe",
                        Email = "jane.doe@email.com",
                        DateOfBirth = new DateTime(1985, 10, 11)
                    },
                    new Consumer()
                    {
                        FirstName = "Maggie",
                        LastName = "Smith",
                        Email = "maggie.smith@email.com",
                        DateOfBirth = new DateTime(1934, 12, 28)
                    });

                context.SaveChanges();
            }

            if (!context.Reviews.Any())
            {
                context.Reviews.AddRange(
                    new Review()
                    {
                        BottleId = 1,
                        ConsumerId = 1,
                        Body = "I like this wine!",
                        Rating = 4,
                        DatePosted = DateTime.Now
                    },
                    new Review()
                    {
                        BottleId = 2,
                        ConsumerId = 1,
                        Body = "OMG! So wow!",
                        Rating = 5,
                        DatePosted = DateTime.Now
                    },
                    new Review()
                    {
                        BottleId = 1,
                        ConsumerId = 2,
                        Body = "I've had better",
                        Rating = 2,
                        DatePosted = DateTime.Now
                    },
                    new Review()
                    {
                        BottleId = 2,
                        ConsumerId = 2,
                        Body = "This wine was okay, I guess",
                        Rating = 3,
                        DatePosted = DateTime.Now
                    });

                context.SaveChanges();
            }

            if (!context.ConsumerBottles.Any())
            {
                context.ConsumerBottles.AddRange(
                    new ConsumerBottle()
                    {
                        ConsumerId = 1,
                        BottleId = 1,
                        Owned = true,
                        Qty = 1,
                        PricePaid = 22,
                        DateAcquired = DateTime.Now,
                        DateOpened = DateTime.Now
                    },
                    new ConsumerBottle()
                    {
                        ConsumerId = 1,
                        BottleId = 2,
                        Owned = true,
                        Qty = 1,
                        PricePaid = 18,
                        DateAcquired = DateTime.Now,
                        DateOpened = DateTime.Now
                    },
                    new ConsumerBottle()
                    {
                        ConsumerId = 2,
                        BottleId = 1,
                        Owned = true,
                        Qty = 1,
                        PricePaid = 21,
                        DateAcquired = DateTime.Now,
                        DateOpened = DateTime.Now
                    },
                    new ConsumerBottle()
                    {
                        ConsumerId = 2,
                        BottleId = 2,
                        Owned = true,
                        Qty = 1,
                        PricePaid = 19,
                        DateAcquired = DateTime.Now,
                        DateOpened = DateTime.Now
                    });

                context.SaveChanges();
            }
        }
    }
}
