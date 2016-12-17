using System;
using System.Collections.Generic;

namespace MyCantinaCore.UI.ViewModels.ConsumerBottle
{
    public class ConsumerBottleDetailsViewModel
    {
        public ConsumerBottleDetailsViewModel()
        {
            GrapeVarieties = new List<string>();
        }

        public int BottleId { get; set; }
        public int ConsumerId { get; set; }
        public string BottleName { get; set; }
        public List<string> GrapeVarieties { get; set; }
        public string WineType { get; set; }
        public string Producer { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public int Qty { get; set; }
        public bool Owned { get; set; }
        public DateTime DateAcquired { get; set; }
        public DateTime? DateOpened { get; set; }
        public decimal PricePaid { get; set; }
    }
}
