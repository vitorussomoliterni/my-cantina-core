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
                        Description = "A fine sparkling",
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
                    GrapeVarietyId = 1,
                    GrapeVarietyName = "Pecorino",
                    GrapeVarietyColour = "White"
                });

                context.SaveChanges();
            }
        }
    }
}
