using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCantinaCore.DataAccess.Models
{
    public class BottleGrapeVariety
    {
        public int BottleId { get; set; }
        public int GrapeVarietyId { get; set; }
        public Bottle  Bottle { get; set; }
        public GrapeVariety GrapeVariety { get; set; }
    }
}
