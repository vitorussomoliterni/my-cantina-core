using System;

namespace MyCantinaCore.Commands.Review
{
    public class ReviewCommand
    {
        public int ConsumerId { get; set; }
        public int BottleId { get; set; }
        public string Body { get; set; }
        public int Rating { get; set; }
    }
}
