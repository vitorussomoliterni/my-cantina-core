using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCantinaCore.UI.ViewModels.ConsumerBottle
{
    public class ConsumerCreateViewModel
    {
        public ConsumerCreateViewModel()
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
