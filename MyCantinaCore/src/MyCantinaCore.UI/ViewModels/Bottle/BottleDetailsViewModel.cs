using System.Collections.Generic;

namespace MyCantinaCore.UI.ViewModels.Bottle
{
    public class BottleDetailsViewModel
    {
        public BottleDetailsViewModel()
        {
            GrapeVarieties = new List<string>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }
        public double AverageRating { get; set; }
        public string WineType { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public List<string> GrapeVarieties { get; set; }
    }
}
