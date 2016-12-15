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
                    Name = "Greco di Tufo",
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
                        Description = "A fine sparkling",
                        WineType = "Sparkling",
                        Region = "Lazio",
                        Country = "Italy",
                        AverageRating = 0
                    });

                context.SaveChanges();
            }
        }
    }
}
