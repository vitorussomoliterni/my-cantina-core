using System;

namespace MyCantinaCore.UI.ViewModels.Review
{
    public class ReviewDetailsViewModel
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int Rating { get; set; }
        public string BottleName { get; set; }
        public string Producer { get; set; }
        public string BottleYear { get; set; }
        public string WineType { get; set; }
        public string ConsumerFullName { get; set; }
        public int ConsumerId { get; set; }
        public int BottleId { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
