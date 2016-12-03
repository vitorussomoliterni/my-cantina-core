using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCantinaCore.DataAccess.Models
{
    public class GrapeVariety
    {
        public GrapeVariety()
        {
            Bottles = new List<Bottle>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Colour { get; set; }
        public List<Bottle> Bottles { get; set; }
    }
}
