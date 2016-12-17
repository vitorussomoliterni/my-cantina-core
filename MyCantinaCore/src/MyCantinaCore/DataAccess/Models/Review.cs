using System;
using System.ComponentModel.DataAnnotations;

namespace MyCantinaCore.DataAccess.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public int ConsumerId { get; set; }
        [Required]
        public int BottleId { get; set; }
        public Consumer Consumer { get; set; }
        public Bottle Bottle { get; set; }
        public string Body { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public DateTime DatePosted { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
