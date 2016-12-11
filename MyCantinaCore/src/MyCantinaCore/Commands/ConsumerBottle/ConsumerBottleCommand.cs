using System;

namespace MyCantinaCore.Commands.ConsumerBottle
{
    public class ConsumerBottleCommand
    {
        public int ConsumerId { get; set; }
        public int BottleId { get; set; }
        public DateTime DateAcquired { get; set; }
        public DateTime? DateOpened { get; set; }
        public int Qty { get; set; }
        public bool Owned { get; set; }
        public double? PricePaid { get; set; }
    }
}
