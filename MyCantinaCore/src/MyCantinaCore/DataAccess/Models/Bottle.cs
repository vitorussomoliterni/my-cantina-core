using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCantinaCore.DataAccess.Models
{
    public class Bottle
    {
        public Bottle()
        {
            BottleGrapeVarieties = new List<BottleGrapeVariety>();
            ConsumerBottles = new List<ConsumerBottle>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public string Producer { get; set; }
        public string Description { get; set; }
        [Required]
        public string WineType { get; set; } // Red, White, Rosé, Sparkling
        public string Region { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public List<BottleGrapeVariety> BottleGrapeVarieties { get; set; }
        public List<ConsumerBottle> ConsumerBottles { get; set; }
        [Required]
        public double AverageRating { get; set; }
    }
}
