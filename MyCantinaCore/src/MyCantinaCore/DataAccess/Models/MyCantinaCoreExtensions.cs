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
        }
    }
}
