﻿using System;

namespace MyCantinaCore.Commands.ConsumerBottle
{
    public class ConsumerBottleCommand
    {
        public ConsumerBottleCommand()
        {
            DateAcquired = new int[3];
            DateOpened = new int[3];
        }

        public int ConsumerId { get; set; }
        public int BottleId { get; set; }
        public int[] DateAcquired { get; set; }
        public int[] DateOpened { get; set; }
        public int Qty { get; set; }
        public bool Owned { get; set; }
        public decimal? PricePaid { get; set; }
    }
}
