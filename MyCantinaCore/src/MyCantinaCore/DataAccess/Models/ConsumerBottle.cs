using System;
using System.ComponentModel.DataAnnotations;

namespace MyCantinaCore.DataAccess.Models
{
    public class ConsumerBottle
    {
        [Required]
        public int ConsumerId { get; set; }
        [Required]
        public int BottleId { get; set; }
        [Required]
        public Consumer Consumer { get; set; }
        [Required]
        public Bottle Bottle { get; set; }
        [Required]
        public DateTime DateAcquired { get; set; }
        public DateTime? DateOpened { get; set; }
        public int Qty { get; set; }
        [Required]
        public bool Owned { get; set; }
        public decimal PricePaid { get; set; }
    }
}
