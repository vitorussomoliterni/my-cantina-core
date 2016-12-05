using System.Collections.Generic;

namespace MyCantinaCore.Commands.Bottle
{
    public class UpdateBottleCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Producer { get; set; }
        public string Description { get; set; }
        public string WineType { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public List<int> GrapeVarietyIds { get; set; }
    }
}
